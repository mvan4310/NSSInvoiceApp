using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    public class DataService
    {
        public string Title = "Home";
        public Action<string> UpdateTitle;

        private static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "data.json");
        //private static string exportPath = Path.Combine(Android.Content.Context.GetExternalFilesDir("PRIVATE_EXTERNAL_STORAGE/Download").AbsolutePath.ToString(), "data.json");

        public Action ToggleSidebar;

        //private UserData loadedData = null;
        public UserData Instance { get; set; }

        public Task<bool> LoadData()
        {
            try
            {

                if (File.Exists(dbPath))
                {
                    using (TextReader reader = new StreamReader(dbPath))
                    {
                        string _data = reader.ReadToEnd();
                        reader.Close();

                        var _loadedData = JsonSerializer.Deserialize<UserData>(_data);
                        Instance = _loadedData;

                        return Task.FromResult(true);
                    }
                }
                else
                {
                    Instance = new UserData();
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

        }

        public void SaveData()
        {
            try
            {
                var _data = JsonSerializer.Serialize(Instance);
                File.CreateText(dbPath).Dispose();
                using (TextWriter writer = new StreamWriter(dbPath, false))
                {
                    writer.WriteLine(_data);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public static async Task SendEmail(string subject, string body, string savePath)
        {
            try
            {
                //string rootPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
                //string savePath = Path.Combine(rootPath, "invoice.pdf");

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body
                };

                message.Attachments.Add(new EmailAttachment(savePath));
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

        public async static Task<string> ConvertInvoiceToPDF(Invoice invoice, List<InvoiceItem> invoiceItems, Customer customer, UserData userData)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                try
                {
                    string rootPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
                    string savePath = Path.Combine(rootPath, "invoice" + invoice.InvoiceNumber + ".pdf");

                    Invoice _invoice = invoice;
                    List<InvoiceItem> _invoiceItems = invoiceItems;
                    Customer _customer = customer;

                    Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Xlsx;
                    //Create a workbook with a worksheet
                    IWorkbook workbook = application.Workbooks.Create(1);

                    //Access first worksheet from the workbook instance.
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Disable gridlines in the worksheet and set margins
                    worksheet.IsGridLinesVisible = false;
                    worksheet.PageSetup.RightMargin = 0.1;
                    worksheet.PageSetup.LeftMargin = 0.1;
                    worksheet.PageSetup.TopMargin = 0.1;
                    worksheet.PageSetup.BottomMargin = 0.1;

                    //Enter values to the cells from A3 to A5
                    worksheet.Range["A3"].Text = userData.CompanyName;
                    worksheet.Range["A4"].Text = userData.CompanyAddress + userData.CompanyCityState + ", " + userData.CompanyZip;
                    worksheet.Range["A5"].Text = userData.CompanyPhone;

                    //Make the text bold
                    worksheet.Range["A3:A5"].CellStyle.Font.Bold = true;

                    //Merge cells
                    worksheet.Range["D1:E1"].Merge();

                    //Enter text to the cell D1 and apply formatting.
                    worksheet.Range["D1"].Text = "INVOICE";
                    worksheet.Range["D1"].CellStyle.Font.Bold = true;
                    worksheet.Range["D1"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
                    worksheet.Range["D1"].CellStyle.Font.Size = 35;

                    //Apply alignment in the cell D1
                    worksheet.Range["D1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                    worksheet.Range["D1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                    //Enter values to the cells from D5 to E8
                    worksheet.Range["D5"].Text = "INVOICE#";
                    worksheet.Range["E5"].Text = "DATE";
                    worksheet.Range["D6"].Number = invoice.InvoiceNumber;
                    worksheet.Range["E6"].Value = invoice.Date.ToShortDateString();
                    worksheet.Range["D7"].Text = "CUSTOMER ID";
                    worksheet.Range["E7"].Text = "DUE";
                    worksheet.Range["D8"].Number = customer.Id;
                    worksheet.Range["E8"].Text = invoice.DueDate.ToShortDateString();

                    //Apply RGB backcolor to the cells from D5 to E8
                    worksheet.Range["D5:E5"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
                    worksheet.Range["D7:E7"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);

                    //Apply known colors to the text in cells D5 to E8
                    worksheet.Range["D5:E5"].CellStyle.Font.Color = ExcelKnownColors.White;
                    worksheet.Range["D7:E7"].CellStyle.Font.Color = ExcelKnownColors.White;

                    //Make the text as bold from D5 to E8
                    worksheet.Range["D5:E8"].CellStyle.Font.Bold = true;

                    //Apply alignment to the cells from D5 to E8
                    worksheet.Range["D5:E8"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range["D5:E5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                    worksheet.Range["D7:E7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                    worksheet.Range["D6:E6"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                    //Enter value and applying formatting in the cell A7
                    worksheet.Range["A7"].Text = "  BILL TO";
                    worksheet.Range["A7"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
                    worksheet.Range["A7"].CellStyle.Font.Bold = true;
                    worksheet.Range["A7"].CellStyle.Font.Color = ExcelKnownColors.White;

                    //Apply alignment
                    worksheet.Range["A7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                    worksheet.Range["A7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                    //Enter values in the cells A8 to A12
                    worksheet.Range["A8"].Text = "";
                    worksheet.Range["A9"].Text = customer.Name;
                    worksheet.Range["A10"].Text = customer.Address;
                    worksheet.Range["A11"].Text = customer.CityState + ", " + customer.Zip;
                    worksheet.Range["A12"].Text = customer.Phone;

                    int start = 16;
                    //int end = 20;
                    //string col = "A";
                    //Enter details of products and prices
                    worksheet.Range["A15"].Text = "  DESCRIPTION";
                    worksheet.Range["C15"].Text = "QTY";
                    worksheet.Range["D15"].Text = "UNIT PRICE";
                    worksheet.Range["E15"].Text = "AMOUNT";

                    foreach (var item in invoiceItems)
                    {
                        worksheet.Range["A" + start].Text = item.Description;
                        worksheet.Range["C" + start].Text = item.Quantity.ToString("C");
                        worksheet.Range["D" + start].Text = item.Price.ToString("C");
                        worksheet.Range["E" + start].Text = (item.Quantity * item.Price).ToString("C");

                        start++;
                    }
                    worksheet.Range["D23"].Text = "Total";

                    //Apply number format
                    //worksheet.Range["D16:E22"].NumberFormat = "$.00";
                    //worksheet.Range["E23"].NumberFormat = "$.00";

                    //Merge column A and B from row 15 to 22
                    worksheet.Range["A15:B15"].Merge();
                    worksheet.Range["A16:B16"].Merge();
                    worksheet.Range["A17:B17"].Merge();
                    worksheet.Range["A18:B18"].Merge();
                    worksheet.Range["A19:B19"].Merge();
                    worksheet.Range["A20:B20"].Merge();
                    worksheet.Range["A21:B21"].Merge();
                    worksheet.Range["A22:B22"].Merge();

                    //Apply incremental formula for column Amount by multiplying Qty and UnitPrice
                    //application.EnableIncrementalFormula = true;
                    //worksheet.Range["E21"].Text = "=C16*D16";

                    //Formula for Sum the total
                    worksheet.Range["E23"].Text = invoice.TotalAmount.ToString("C");

                    //Apply borders
                    worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                    worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                    worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                    worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;
                    worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                    worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                    worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
                    worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;

                    //Apply font setting for cells with product details
                    worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Arial";
                    worksheet.Range["A3:E23"].CellStyle.Font.Size = 10;
                    worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
                    worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
                    worksheet.Range["D23:E23"].CellStyle.Font.Bold = true;

                    //Apply cell color
                    worksheet.Range["A15:E15"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);

                    //Apply alignment to cells with product details
                    worksheet.Range["A15"].CellStyle.HorizontalAlignment = Syncfusion.XlsIO.ExcelHAlign.HAlignLeft;
                    worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                    //Apply row height and column width to look good
                    worksheet.Range["A1"].ColumnWidth = 36;
                    worksheet.Range["B1"].ColumnWidth = 11;
                    worksheet.Range["C1"].ColumnWidth = 8;
                    worksheet.Range["D1:E1"].ColumnWidth = 18;
                    worksheet.Range["A1"].RowHeight = 47;
                    worksheet.Range["A2"].RowHeight = 15;
                    worksheet.Range["A3:A4"].RowHeight = 15;
                    worksheet.Range["A5"].RowHeight = 18;
                    worksheet.Range["A6"].RowHeight = 29;
                    worksheet.Range["A7"].RowHeight = 18;
                    worksheet.Range["A8"].RowHeight = 15;
                    worksheet.Range["A9:A14"].RowHeight = 15;
                    worksheet.Range["A15:A23"].RowHeight = 18;


                    //Initialize XlsIO renderer.
                    XlsIORenderer renderer = new XlsIORenderer();

                    //Initialize PDF document
                    PdfDocument pdfDocument = renderer.ConvertToPDF(workbook);
                    PdfPageSettings pageSettings = new PdfPageSettings() {Orientation = PdfPageOrientation.Portrait, Size = PdfPageSize.Letter  };
                    pdfDocument.PageSettings = pageSettings;
                    pdfDocument.PageSettings.Margins.All = 0.5f;

                    File.Create(savePath).Dispose();
                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        pdfDocument.Save(tempStream);
                        File.WriteAllBytes(savePath, tempStream.ToArray());
                    }

                    return savePath;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    return "";
                }

                
            }
        }

        
    }
}
