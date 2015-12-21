using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POCOGenerator
{
    public partial class TypeMappingForm : Form
    {
        public TypeMappingForm()
        {
            InitializeComponent();
            LoadTypeMappingForm();
        }

        private static readonly Color colorKeyword = Color.FromArgb(0, 0, 255);
        private static readonly Color colorUserType = Color.FromArgb(43, 145, 175);

        private void LoadTypeMappingForm()
        {
            txtTypeMappingEditor.AppendText("┌──────────────────┬──────────────────────────────────────────┐");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│    SQL Server    │                   .NET                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("├──────────────────┼──────────────────────────────────────────┤");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("bigint", colorKeyword);
            txtTypeMappingEditor.AppendText("           │ ");
            txtTypeMappingEditor.AppendText("long", colorKeyword);
            txtTypeMappingEditor.AppendText("                                     │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("binary", colorKeyword);
            txtTypeMappingEditor.AppendText("           │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("[]");
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("bit", colorKeyword);
            txtTypeMappingEditor.AppendText("              │ ");
            txtTypeMappingEditor.AppendText("bool", colorKeyword);
            txtTypeMappingEditor.AppendText("                                     │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("char", colorKeyword);
            txtTypeMappingEditor.AppendText("             │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("date", colorKeyword);
            txtTypeMappingEditor.AppendText("             │ ");
            txtTypeMappingEditor.AppendText("DateTime", colorUserType);
            txtTypeMappingEditor.AppendText("                                 │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("datetime", colorKeyword);
            txtTypeMappingEditor.AppendText("         │ ");
            txtTypeMappingEditor.AppendText("DateTime", colorUserType);
            txtTypeMappingEditor.AppendText("                                 │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("datetime2", colorKeyword);
            txtTypeMappingEditor.AppendText("        │ ");
            txtTypeMappingEditor.AppendText("DateTime", colorUserType);
            txtTypeMappingEditor.AppendText("                                 │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("datetimeoffset", colorKeyword);
            txtTypeMappingEditor.AppendText("   │ ");
            txtTypeMappingEditor.AppendText("DateTimeOffset", colorUserType);
            txtTypeMappingEditor.AppendText("                           │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("decimal", colorKeyword);
            txtTypeMappingEditor.AppendText("          │ ");
            txtTypeMappingEditor.AppendText("decimal", colorKeyword);
            txtTypeMappingEditor.AppendText("                                  │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("filestream", colorKeyword);
            txtTypeMappingEditor.AppendText("       │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("[]");
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("float", colorKeyword);
            txtTypeMappingEditor.AppendText("            │ ");
            txtTypeMappingEditor.AppendText("double", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("geography", colorKeyword);
            txtTypeMappingEditor.AppendText("        │ ");
            txtTypeMappingEditor.AppendText("Microsoft.SqlServer.Types.");
            txtTypeMappingEditor.AppendText("SqlGeography", colorUserType);
            txtTypeMappingEditor.AppendText("   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("geometry", colorKeyword);
            txtTypeMappingEditor.AppendText("         │ ");
            txtTypeMappingEditor.AppendText("Microsoft.SqlServer.Types.");
            txtTypeMappingEditor.AppendText("SqlGeometry", colorUserType);
            txtTypeMappingEditor.AppendText("    │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("hierarchyid", colorKeyword);
            txtTypeMappingEditor.AppendText("      │ ");
            txtTypeMappingEditor.AppendText("Microsoft.SqlServer.Types.");
            txtTypeMappingEditor.AppendText("SqlHierarchyId", colorUserType);
            txtTypeMappingEditor.AppendText(" │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("image", colorKeyword);
            txtTypeMappingEditor.AppendText("            │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("[]");
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("int", colorKeyword);
            txtTypeMappingEditor.AppendText("              │ ");
            txtTypeMappingEditor.AppendText("int", colorKeyword);
            txtTypeMappingEditor.AppendText("                                      │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("money", colorKeyword);
            txtTypeMappingEditor.AppendText("            │ ");
            txtTypeMappingEditor.AppendText("decimal", colorKeyword);
            txtTypeMappingEditor.AppendText("                                  │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("nchar", colorKeyword);
            txtTypeMappingEditor.AppendText("            │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("ntext", colorKeyword);
            txtTypeMappingEditor.AppendText("            │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("numeric", colorKeyword);
            txtTypeMappingEditor.AppendText("          │ ");
            txtTypeMappingEditor.AppendText("decimal", colorKeyword);
            txtTypeMappingEditor.AppendText("                                  │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("nvarchar", colorKeyword);
            txtTypeMappingEditor.AppendText("         │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("real", colorKeyword);
            txtTypeMappingEditor.AppendText("             │ ");
            txtTypeMappingEditor.AppendText("Single", colorUserType);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("rowversion", colorKeyword);
            txtTypeMappingEditor.AppendText("       │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("[]");
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("smalldatetime", colorKeyword);
            txtTypeMappingEditor.AppendText("    │ ");
            txtTypeMappingEditor.AppendText("DateTime", colorUserType);
            txtTypeMappingEditor.AppendText("                                 │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("smallint", colorKeyword);
            txtTypeMappingEditor.AppendText("         │ ");
            txtTypeMappingEditor.AppendText("short", colorKeyword);
            txtTypeMappingEditor.AppendText("                                    │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("smallmoney", colorKeyword);
            txtTypeMappingEditor.AppendText("       │ ");
            txtTypeMappingEditor.AppendText("decimal", colorKeyword);
            txtTypeMappingEditor.AppendText("                                  │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("sql_variant", colorKeyword);
            txtTypeMappingEditor.AppendText("      │ ");
            txtTypeMappingEditor.AppendText("object", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("text", colorKeyword);
            txtTypeMappingEditor.AppendText("             │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("time", colorKeyword);
            txtTypeMappingEditor.AppendText("             │ ");
            txtTypeMappingEditor.AppendText("TimeSpan", colorUserType);
            txtTypeMappingEditor.AppendText("                                 │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("timestamp", colorKeyword);
            txtTypeMappingEditor.AppendText("        │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("[]");
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("tinyint", colorKeyword);
            txtTypeMappingEditor.AppendText("          │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("                                     │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("uniqueidentifier", colorKeyword);
            txtTypeMappingEditor.AppendText(" │ ");
            txtTypeMappingEditor.AppendText("Guid", colorUserType);
            txtTypeMappingEditor.AppendText("                                     │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("varbinary", colorKeyword);
            txtTypeMappingEditor.AppendText("        │ ");
            txtTypeMappingEditor.AppendText("byte", colorKeyword);
            txtTypeMappingEditor.AppendText("[]");
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("varchar", colorKeyword);
            txtTypeMappingEditor.AppendText("          │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("xml", colorKeyword);
            txtTypeMappingEditor.AppendText("              │ ");
            txtTypeMappingEditor.AppendText("string", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("│ ");
            txtTypeMappingEditor.AppendText("else");
            txtTypeMappingEditor.AppendText("             │ ");
            txtTypeMappingEditor.AppendText("object", colorKeyword);
            txtTypeMappingEditor.AppendText("                                   │");
            txtTypeMappingEditor.AppendText(Environment.NewLine);

            txtTypeMappingEditor.AppendText("└──────────────────┴──────────────────────────────────────────┘");
        }
    }
}