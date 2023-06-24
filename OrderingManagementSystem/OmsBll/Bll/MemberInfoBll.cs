using CaterDal;
using domain.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class MemberInfoBll
    {
        private MemberInfoDal _memberInfoDal = new MemberInfoDal();

        public List<MemberInfo> List(Dictionary<string, string> dic)
        {
            return _memberInfoDal.GetList(dic);
        }

        public int SaveMemberInfo(MemberInfo memberInfo)
        {
            return _memberInfoDal.Insert(memberInfo);
        }

        public int UpdateMemberInfo(MemberInfo memberInfo)
        {
            return _memberInfoDal.Update(memberInfo);
        }

        public int DeleteMemberInfo(int id)
        {
            return _memberInfoDal.Delete(id);
        }

        public void ExportExecl(string fileName)
        {
            DataTable dt = _memberInfoDal.GetDataTable();

            IWorkbook workbook;
            string fileExt = Path.GetExtension(fileName).ToLower();
            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
                return;
            }

            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //保存为Excel文件  
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
        }

    }
}
