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
            Thread thread_NowDate = new Thread(StartThread);

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
    }
}
