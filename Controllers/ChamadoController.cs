using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChamadoController : ControllerBase
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoController(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository ?? throw new ArgumentNullException(nameof(chamadoRepository));
        }

        // ==================== EXISTENTES ==================== //
        [HttpPost]
        public IActionResult Add([FromBody] ChamadoViewModel chamadoView)
        {
            if (chamadoView == null)
                return BadRequest("Dados do chamado inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chamado = new Chamado(
                chamadoView.Id_chamado,
                chamadoView.Descricao,
                chamadoView.Numero_chamado,
                chamadoView.Data_abertura == default ? DateTime.Now : chamadoView.Data_abertura,
                string.IsNullOrWhiteSpace(chamadoView.Status) ? "Pendente" : chamadoView.Status,
                chamadoView.Id_usuario,
                chamadoView.Id_categoria
            );

            _chamadoRepository.Add(chamado);

            return CreatedAtAction(nameof(GetById), new { id = chamado.Id_chamado }, chamado);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var chamados = _chamadoRepository.Get();

            if (chamados == null || !chamados.Any())
                return NotFound("Nenhum chamado encontrado.");

            return Ok(chamados);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var chamado = _chamadoRepository.GetById(id);

            if (chamado == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            return Ok(chamado);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult GetByUsuario(int idUsuario)
        {
            var chamadosUsuario = _chamadoRepository.GetByUsuario(idUsuario);

            if (chamadosUsuario == null || !chamadosUsuario.Any())
                return NotFound($"Nenhum chamado encontrado para o usuário com ID {idUsuario}.");

            return Ok(chamadosUsuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ChamadoViewModel chamadoView)
        {
            if (chamadoView == null)
                return BadRequest("Dados do chamado inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _chamadoRepository.GetById(id);
            if (existente == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            var chamadoAtualizado = new Chamado(
                id,
                chamadoView.Descricao,
                chamadoView.Numero_chamado,
                chamadoView.Data_abertura == default ? existente.Data_abertura : chamadoView.Data_abertura,
                string.IsNullOrWhiteSpace(chamadoView.Status) ? existente.Status : chamadoView.Status,
                chamadoView.Id_usuario,
                chamadoView.Id_categoria
            );

            _chamadoRepository.Update(chamadoAtualizado);

            return Ok(chamadoAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var chamado = _chamadoRepository.GetById(id);
            if (chamado == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            _chamadoRepository.Delete(id);
            return NoContent();
        }

        // ==================== NOVOS MÉTODOS ==================== //

        // GET: api/v1/Chamado/ExportExcel
        [HttpGet("ExportExcel")]
        public IActionResult ExportExcel()
        {
            var dados = _chamadoRepository.Get();

            using var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Chamados");

            // Cabeçalho
            ws.Cell(1, 1).Value = "ID";
            ws.Cell(1, 2).Value = "Descrição";
            ws.Cell(1, 3).Value = "Número Chamado";
            ws.Cell(1, 4).Value = "Data Abertura";
            ws.Cell(1, 5).Value = "Status";
            ws.Cell(1, 6).Value = "ID Usuário";
            ws.Cell(1, 7).Value = "ID Categoria";

            ws.Range(1, 1, 1, 7).Style.Font.Bold = true;
            ws.Range(1, 1, 1, 7).Style.Fill.BackgroundColor = XLColor.LightGray;

            int row = 2;
            foreach (var c in dados)
            {
                ws.Cell(row, 1).Value = c.Id_chamado;
                ws.Cell(row, 2).Value = c.Descricao;
                ws.Cell(row, 3).Value = c.Numero_chamado;
                ws.Cell(row, 4).Value = c.Data_abertura.ToString("dd/MM/yyyy HH:mm");
                ws.Cell(row, 5).Value = c.Status;
                ws.Cell(row, 6).Value = c.Id_usuario;
                ws.Cell(row, 7).Value = c.Id_categoria;
                row++;
            }

            ws.Columns().AdjustToContents();

            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            var bytes = ms.ToArray();

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "chamados.xlsx");
        }

        // GET: api/v1/Chamado/ExportPdf
        [HttpGet("ExportPdf")]
        public IActionResult ExportPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var dados = _chamadoRepository.Get();

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Relatório de Chamados").SemiBold().FontSize(16).AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // ID
                            cols.RelativeColumn(3); // Descrição
                            cols.RelativeColumn(2); // Número
                            cols.RelativeColumn(2); // Status
                            cols.RelativeColumn(1); // Usuário
                            cols.RelativeColumn(1); // Categoria
                        });

                        // Cabeçalho
                        table.Header(header =>
                        {
                            IContainer CellHeader(IContainer c) => c.DefaultTextStyle(x => x.SemiBold()).Padding(4).BorderBottom(1).BorderColor(Colors.Grey.Medium);

                            header.Cell().Element(CellHeader).Text("ID");
                            header.Cell().Element(CellHeader).Text("Descrição");
                            header.Cell().Element(CellHeader).Text("Número");
                            header.Cell().Element(CellHeader).Text("Status");
                            header.Cell().Element(CellHeader).Text("ID Usuário");
                            header.Cell().Element(CellHeader).Text("ID Categoria");
                        });

                        // Linhas
                        IContainer CellBody(IContainer c) => c.Padding(4);
                        foreach (var c in dados)
                        {
                            table.Cell().Element(CellBody).Text(c.Id_chamado.ToString());
                            table.Cell().Element(CellBody).Text(c.Descricao);
                            table.Cell().Element(CellBody).Text(c.Numero_chamado.ToString());
                            table.Cell().Element(CellBody).Text(c.Status);
                            table.Cell().Element(CellBody).Text(c.Id_usuario.ToString());
                            table.Cell().Element(CellBody).Text(c.Id_categoria.ToString());
                        }
                    });

                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Gerado em ").Light();
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    });
                });
            });

            var pdfBytes = doc.GeneratePdf();
            return File(pdfBytes, "application/pdf", "chamados.pdf");
        }
    }
}
