using AutoMapper;
using CM.Data.Infrastructure;
using CM.Data.ViewModels.Medicine;
using CM.Model.Models.Medicine;
using CM.Service.ServiceInterfaces;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CM.Service.Services
{
    public class MedicineStockService : IMedicineStockService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IEnumerable<Medicine> Medicines { get; set; }
        private IEnumerable<MedicineViewModel> MedicineViewModels { get; set; }
        private IEnumerable<Category> Categories;
        private IEnumerable<Manufacturer> Manufacturers;

        public MedicineStockService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            Categories = unitOfWork.CategoryRepository.Fetch();
            Manufacturers = unitOfWork.ManufacturerRepository.Fetch();
        }

        public byte[] GetStockInExcel(out string fileName)
        {
            try
            {
                Medicines = new List<Medicine>();
                MedicineViewModels = new List<MedicineViewModel>();
                Medicines = unitOfWork.MedicineRepository.Fetch(m=>m.StockLevel<=m.OrderLevel);
                if (Medicines != null)
                {
                    MedicineViewModels = mapper.Map(Medicines, MedicineViewModels);
                    using (var excelPackage = new ExcelPackage())
                    {
                        var package = new ExcelPackage();
                        package.Workbook.Properties.Title = "Stock Report";
                        package.Workbook.Properties.Author = "Chetan Kumar";

                        var numberformat = "#,##0";
                        var dataCellStyleName = "TableNumber";
                        var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
                        numStyle.Style.Numberformat.Format = numberformat;

                        var worksheet = package.Workbook.Worksheets.Add("Stock");

                        //First add the headers
                        worksheet.Cells["A1"].Value = "Name";
                        worksheet.Cells["B1"].Value = "Price";
                        worksheet.Cells["C1"].Value = "MRP";
                        worksheet.Cells["D1"].Value = "Order Level";
                        worksheet.Cells["E1"].Value = "Stock Level";
                        worksheet.Cells["F1"].Value = "Category Name";
                        worksheet.Cells["G1"].Value = "Manufacturer Name";
                        int row = 2;

                        //Add values

                        foreach (var medicine in MedicineViewModels)
                        {
                            medicine.CategoryName = Categories.FirstOrDefault(c => c.Id == medicine.CategoryFK)?.CategoryName;
                            medicine.ManufacturerName = Manufacturers.FirstOrDefault(m => m.Id == medicine.ManufacturerFk)?.ManufacturerName;

                            worksheet.Cells[string.Format("A{0}", row)].Value = medicine.Name;
                            worksheet.Cells[string.Format("B{0}", row)].Value = medicine.Price;
                            worksheet.Cells[string.Format("C{0}", row)].Value = medicine.MRP;
                            worksheet.Cells[string.Format("D{0}", row)].Value = medicine.OrderLevel;
                            worksheet.Cells[string.Format("E{0}", row)].Value = medicine.StockLevel;
                            worksheet.Cells[string.Format("F{0}", row)].Value = medicine.CategoryName;
                            worksheet.Cells[string.Format("G{0}", row)].Value = medicine.ManufacturerName;
                            row++;
                        }

                        // Add to table / Add summary row
                        var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: MedicineViewModels.Count() + 1, toColumn: 7), "Data");
                        tbl.ShowHeader = true;
                        tbl.TableStyle = TableStyles.Dark9;
                        tbl.ShowTotal = true;
                        tbl.Columns[1].DataCellStyleName = dataCellStyleName;
                        tbl.Columns[2].DataCellStyleName = dataCellStyleName;
                        tbl.Columns[1].TotalsRowFunction = RowFunctions.Sum;
                        tbl.Columns[2].TotalsRowFunction = RowFunctions.Sum;

                        worksheet.Cells["A:AZ"].AutoFitColumns();
                        fileName = DateTime.Now.Date.ToString("dd-MM-yy") + "Medicine Stock List.xlsx";
                        return package.GetAsByteArray();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            fileName = string.Empty;
            return null;
        }
    }
}
