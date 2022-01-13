using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Giao tiếp qua Serial
using System.IO;
using System.IO.Ports;
using System.Xml;

// Thêm ZedGraph
using ZedGraph;

namespace CsharpInterface
{
    public
    partial class Form1 : Form
    {
        string SDatas = String.Empty; // Khai báo chuỗi để lưu dữ liệu cảm biến gửi qua Serial
        string SRealTime = String.Empty; // Khai báo chuỗi để lưu thời gian gửi qua Serial
        int status = 0; // Khai báo biến để xử lý sự kiện vẽ đồ thị
        double realtime = 0; //Khai báo biến thời gian để vẽ đồ thị
        double datas = 0; //Khai báo biến dữ liệu cảm biến để vẽ đồ thị

        public
            Form1()
        {
            InitializeComponent();
        }

        private
            void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames(); // Lấy nguồn cho comboBox là tên của cổng COM
            comboBox1.Text = Properties.Settings.Default.ComName; // Lấy ComName đã làm ở bước 5 cho comboBox

            // Khởi tạo ZedGraph
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Cyclic - Voltammetry Graph";
            myPane.XAxis.Title.Text = "Potential (mV)";
            myPane.YAxis.Title.Text = "Current (mA)";

            RollingPointPairList list = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve("Dữ liệu", list, Color.Red, SymbolType.None);

            myPane.XAxis.Scale.Min = -10;
            myPane.XAxis.Scale.Max = 10;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 5;
            myPane.YAxis.Scale.Min = -20;
            myPane.YAxis.Scale.Max = 20;

            myPane.AxisChange();
        }

        // Hàm Tick này sẽ bắt sự kiện cổng Serial mở hay không
        private
            void timer1_Tick(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                progressBar1.Value = 0;
                //ClearZedGraph();
            }
            else if (serialPort1.IsOpen)
            {
                progressBar1.Value = 100;
                Draw();
                Data_Listview();
                status = 0;

            }
        }
            // Hàm này lưu lại cổng COM đã chọn cho lần kết nối
            private
            void SaveSetting()
        {
            Properties.Settings.Default.ComName = comboBox1.Text;
            Properties.Settings.Default.Save();
        }

        // Nhận và xử lý string gửi từ Serial
        private
            void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string[] arrList = serialPort1.ReadLine().Split('|'); // Đọc một dòng của Serial, cắt chuỗi khi gặp ký tự gạch đứng
                SRealTime = arrList[0]; // Chuỗi đầu tiên lưu vào SRealTime
                SDatas = arrList[1]; // Chuỗi thứ hai lưu vào SDatas

                double.TryParse(SDatas, out datas); // Chuyển đổi sang kiểu double
                double.TryParse(SRealTime, out realtime);
                realtime = realtime / 100; // Đối ms sang s
                //datas = datas ;
                status = 1; // Bắt sự kiện xử lý xong chuỗi, đổi starus về 1 để hiển thị dữ liệu trong ListView và vẽ đồ thị
            }
            catch
            {
                return;
            }
        }

        // Hiển thị dữ liệu trong ListView
        private
            void Data_Listview()
        {
            if (status == 0)
                return;
            else
            {
                ListViewItem item = new ListViewItem(realtime.ToString()); // Gán biến realtime vào cột đầu tiên của ListView
                item.SubItems.Add(datas.ToString());
                listView1.Items.Add(item); // Gán biến datas vào cột tiếp theo của ListView
                                           // Không nên gán string SDatas vì khi xuất dữ liệu sang Excel sẽ là dạng string, không thực hiện các phép toán được

                listView1.Items[listView1.Items.Count - 1].EnsureVisible(); // Hiện thị dòng được gán gần nhất ở ListView, tức là mình cuộn ListView theo dữ liệu gần nhất đó
            }
        }

        // Vẽ đồ thị
        private
            void Draw()
        {

            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
                return;

            LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;

            if (curve == null)
                return;

            IPointListEdit list = curve.Points as IPointListEdit;

            if (list == null)
                return;

            list.Add(realtime, datas); // Thêm điểm trên đồ thị

            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            Scale yScale = zedGraphControl1.GraphPane.YAxis.Scale;

            // Tự động Scale theo trục x
            if (realtime > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = realtime + xScale.MajorStep;
                xScale.Min = xScale.Max - 30;
            }

            // Tự động Scale theo trục y
            if (datas > yScale.Max - yScale.MajorStep)
            {
                yScale.Max = datas + yScale.MajorStep;
            }
            else if (datas < yScale.Min + yScale.MajorStep)
            {
                yScale.Min = datas - yScale.MajorStep;
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();

            //ZedGraph.GraphPane myPane = zedGraphControl1.GraphPane;
            //myPane.XAxis.Scale.Min = 0.0;
            //zedGraphControl1.AxisChange();
            //zedGraphControl1.RestoreScale(myPane);
            //zedGraphControl1.ZoomOut(pane);
        }

        // Xóa đồ thị, với ZedGraph thì phải khai báo lại như ở hàm Form1_Load, nếu không sẽ không hiển thị
        private
            void ClearZedGraph()
        {
            zedGraphControl1.GraphPane.CurveList.Clear(); // Xóa đường
            zedGraphControl1.GraphPane.GraphObjList.Clear(); // Xóa đối tượng

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Current-Voltage chart for Biomedical Testing";
            myPane.XAxis.Title.Text = "Potential (mV)";
            myPane.YAxis.Title.Text = "Current (mA)";

            RollingPointPairList list = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve("Dữ liệu", list, Color.Red, SymbolType.None);

            myPane.XAxis.Scale.Min = -10;
            myPane.XAxis.Scale.Max = 10;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 5;
            myPane.YAxis.Scale.Min = -20;
            myPane.YAxis.Scale.Max = 20;

            zedGraphControl1.AxisChange();
        }

        // Hàm xóa dữ liệu
        private
            void ResetValue()
        {
            realtime = 0;
            datas = 0; 
            SDatas = String.Empty;
            SRealTime = String.Empty;
            status = 0; // Chuyển status về 0
        }

        // Hàm lưu ListView sang Excel
        private
            void SaveToExcel()
        {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            xla.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;

            // Đặt tên cho hai ô A1. B1 lần lượt là "Thời gian (s)" và "Dữ liệu", sau đó tự động dãn độ rộng
            Microsoft.Office.Interop.Excel.Range rg = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("A1", "B1");
            ws.Cells[1, 1] = "Potential";
            ws.Cells[1, 2] = "Current";
            rg.Columns.AutoFit();

            // Lưu từ ô đầu tiên của dòng thứ 2, tức ô A2
            int i = 2;
            int j = 1;

            foreach (ListViewItem comp in listView1.Items)
            {
                ws.Cells[i, j] = comp.Text.ToString();
                foreach (ListViewItem.ListViewSubItem drv in comp.SubItems)
                {
                    ws.Cells[i, j] = drv.Text.ToString();
                    j++;
                }
                j = 1;
                i++;
            }
        }

        // Sự kiện nhấn nút btConnect
        private
            void btConnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("2"); //Gửi ký tự "2" qua Serial, tương ứng với state = 2
                serialPort1.Close();
                btConnect.Text = "Kết nối";
                btExit.Enabled = true;
                SaveSetting(); // Lưu cổng COM vào ComName
            }
            else
            {
                serialPort1.PortName = comboBox1.Text; // Lấy cổng COM
                serialPort1.BaudRate = 230400; // Baudrate là 9600, trùng với baudrate của Arduino
                //serialPort1.Write("2"); //Gửi ký tự "2" qua Serial, tương ứng với state = 2
                //serialPort1.Write("1"); //Gửi ký tự "2" qua Serial, tương ứng với state = 1
                try
                {
                    serialPort1.Open();
                    btConnect.Text = "Ngắt kết nối";
                    btExit.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("Không thể mở cổng" + serialPort1.PortName, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện nhấn nút btExxit
        private
            void btExit_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (traloi == DialogResult.OK)
            {
                Application.Exit(); // Đóng ứng dụng
            }
        }

        // Sự kiện nhấn nút btSave
        private
            void btSave_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có muốn lưu số liệu?", "Lưu", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (traloi == DialogResult.OK)
            {
                SaveToExcel(); // Thực thi hàm lưu ListView sang Excel
            }
        }

        // Sự kiện nhấn nút btCheck
        private
            void btCheck_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("1"); //Gửi ký tự "1" qua Serial, chạy hàm tạo Random ở Arduino
            }
            else
                MessageBox.Show("Bạn không thể chạy khi chưa kết nối với thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        // Sự kiện nhấn nút btPause
        private
            void btPause_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("0"); //Gửi ký tự "0" qua Serial, Dừng Arduino
            }
            else
                MessageBox.Show("Bạn không thể dừng khi chưa kết nối với thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Sự kiện nhấn nút Clear
        private
            void btClear_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                DialogResult traloi;
                traloi = MessageBox.Show("Bạn có chắc muốn xóa?", "Xóa dữ liệu", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (traloi == DialogResult.OK)
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("2"); //Gửi ký tự "2" qua Serial
                        listView1.Items.Clear(); // Xóa listview

                        //Xóa đường trong đồ thị
                        ClearZedGraph();

                        //Xóa dữ liệu trong Form
                        ResetValue();
                    }
                    else
                        MessageBox.Show("Bạn không thể dừng khi chưa kết nối với thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Bạn không thể xóa khi chưa kết nối với thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Sự kiện nhấn nút Run
        private
            void btRun_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear(); // Xóa listview
            ClearZedGraph();
            //ResetValue();
            Draw();
            
        }       
                   
    }
}