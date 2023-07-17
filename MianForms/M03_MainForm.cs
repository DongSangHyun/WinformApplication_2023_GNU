
using FormList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MianForms
{
    public partial class M03_MainForm : Form
    {
        Thread thread_NowDate;

        public M03_MainForm()
        {
            InitializeComponent();
        }
        public M03_MainForm(string sUserName)
        {
            InitializeComponent();
            stsUserName.Text = sUserName;
        }

        #region < 폼 Load > 
        private void M03_MainForm_Load(object sender, EventArgs e)
        {
            // 메인 화면이 오픈이 될때.
            // 1. Timer 를 이용하여 스레드 생성.  
            // timNowDate.Enabled = true;

            // 2. Thread 클래스를 통하여 스레드 생성. 
            // Thread 
            // - 프로세스 내부에서 생성되는 메인 처리 흐름 과는 별개의 흐름(프로세스) 를 추가 생성 함으로서
            //   하나의 프로세스 외에 여러가지 일을 동시에 수행하는 기능. (비동기)

            // 2-1. 스레드가 실행 할 메서드 설정. 
            ThreadStart StartThread = new ThreadStart(TimeShow);

            // 2-2 델리게이트 를 실행할 Thread 클래스 생성. 
            thread_NowDate = new Thread(StartThread);

            // 2-3 스레드 시작. 
            thread_NowDate.Start();
        }
        #endregion

        #region < Thread Method > 
        private void timNowDate_Tick(object sender, EventArgs e)
        {
            // Timer 를 이용하는 방법. 
            // Interver 1000 ( 1초 간격으로 ) 실행할 로직. 
            // 현재 일시 를 나타내기 . 
            stsNowDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void TimeShow()
        {
            // 스레드가 실행 할 로직. 
            while (true) 
            {
                Thread.Sleep(1000);
                stsNowDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        #endregion

        #region < 종료 버튼 클릭 > 
        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit(); // 응용 프로그램의 종료.
        }


        private void M03_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 프로그램이 종료 될때 . 
            // 1. 프로그램 종료 여부를 확인. 
            DialogResult Result = MessageBox.Show("프로그램을 종료 하시겠습니까 ?","프로그램종료",MessageBoxButtons.YesNo);
            if (Result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            } 

            // 2. 프로그램 종료 여부 의  결과 가 Yes 인경우. 
            // 구동 되고 있는 스레드를 종료.
            if (thread_NowDate.IsAlive) thread_NowDate.Abort(); // 스레드 종료.
        }
        #endregion

        private void BaseM_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // 기준정보 리스트에 있는 매뉴 클릭 할 경우.

            if (e.ClickedItem.Name == "ItemMaster")
            {
                ItemMaster itemmaster = new ItemMaster();
                itemmaster.Show();
            }

            else if (e.ClickedItem.Name == "UserMaster")
            {
                UserMaster usermaster = new UserMaster();
                usermaster.Show();
            }
        }
    }
}
