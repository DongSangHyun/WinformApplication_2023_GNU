
using FormList;
using Services_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

            #region < 매뉴 의 클래스를 하나하나 호출 해야 하는 경우 > 
            //if (e.ClickedItem.Name == "ItemMaster")
            //{
            //    tabMyTab.AddForm(new ItemMaster());
            //}

            //else if (e.ClickedItem.Name == "UserMaster")
            //{
            //    tabMyTab.AddForm(new UserMaster());
            //}

            // 매뉴에서 호출해야 하는 클래스(윈폼 화면) 가 증가 할 수록 
            // 로직이 길어지고 유지보수 및 확장성 등 비효율적인 코딩이 될 수 있다
            #endregion

            #region < 매뉴의 Name 문자열 을 가지고있는 클래스를 호출할 경우 > 

            // 이미 탭 페이지에 등록 되어있는 화면 을 매뉴에서 선택한 경우 
            // 해당 탭 페이지를 활성화. 
            // 그렇지 않을 경우 탭페이지에 추가 하여 탭컨트롤에 표현.
            
            for (int i = 0; i < tabMyTab.TabPages.Count; i++)
            {
                if (tabMyTab.TabPages[i].Name == e.ClickedItem.Name)
                {
                    // 텝 페이지의 이름과 매뉴의 이름이 같다. ( 등록된 페이지를 활성화 ) 
                    tabMyTab.SelectedTab = tabMyTab.TabPages[i];
                    return;
                }
            }

            // Assembly : 프로젝트 (dll) 파일 의 클래스 를 추출 하고 관리 할수 있는 클래스.

            // DLL 파일이 있는 위치 를 찾기 . 
            // Application.StartupPath : 응용 프로그램이 실행 되는 위치 . "\\" = \ 
            Assembly assem = Assembly.LoadFrom(Application.StartupPath + "\\" + "FormList.DLL");

            // Type : 파일 형식으로 되어있는 클래스 유형을 Winform 형식의 Form 클래스 로 변형 시켜 주는 클래스.
            Type typeForm = assem.GetType("FormList." + e.ClickedItem.Name.ToString(), true);

            // 파일에서 추출 한 클래스 를 Form 형식으로 변형. 
            Form ShowForm = (Form)Activator.CreateInstance(typeForm);

            // 추출한 클래스를 Form 형식으로 만든 후 TabControl 에 추가. 
            tabMyTab.AddForm(ShowForm);
            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 닫기 버튼을 클릭 시 기능. 
            if (tabMyTab.TabPages.Count == 0) return;
            tabMyTab.SelectedTab.Dispose();
        }

        #region < 툴바의 개별적인 기능 구현 소스 > 
        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    if (tabMyTab.TabPages.Count == 0) return;

        //    #region < 다형성을 적용하지 않은 예 >
        //    //if (tabMyTab.SelectedTab.Name == "ItemMaster")
        //    //{
        //    //    // 지금 열려있는 탭 페이지의 화면을 품목마스터 로 받은 후 조회를 실행
        //    //    // tabMyTab.SelectedTab.Controls[0] : 텝 페이지 에 등록 된 0순위의 클래스. (ItemMaster)
        //    //    ItemMaster item_master = tabMyTab.SelectedTab.Controls[0] as ItemMaster;
        //    //    item_master.DoInquire();
        //    //}
        //    //if (tabMyTab.SelectedTab.Name == "UserMaster")
        //    //{
        //    //    // 지금 열려있는 탭 페이지의 화면을 사용자마스터 로 받은 후 조회를 실행
        //    //    // tabMyTab.SelectedTab.Controls[0] : 텝 페이지 에 등록 된 0순위의 클래스. (ItemMaster)
        //    //    UserMaster user_master = tabMyTab.SelectedTab.Controls[0] as UserMaster;
        //    //    user_master.DoInquire();
        //    //}

        //    // 불필요하게 클래스가 많아질경우 소스가 길어질수 있다 비효율적.
        //    #endregion

        //    #region < 다형성을 적용한 예제 >
        //    //BaseChildForm BS = tabMyTab.SelectedTab.Controls[0] as BaseChildForm;

        //    // ItemMaster, UserMaster 클래스 가 부모 클래스 인 BasChidForm 클래스로 UPCasting
        //    // BCH 객체 는  ItemMaster, UserMaster 의 기능을 대신 수행할 수 있는 다형성 이 구현된 상태

        //    BaseChildForm BCH = (BaseChildForm)tabMyTab.SelectedTab.Controls[0];
        //    BCH.DoInquire();

        //    // 다형성 
        //    // 자식 클래스 가 부모 클래스 형으로 변환 되어 
        //    // 자식 클래스 가 가지고 있는 기능을 부모 클래스가 대신 수행할수 있도록 하는 개념
        //    // 다형성을 구현 하는 기능 Upcasting;
        //    #endregion
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    // 다형성을 통한 자식 클래스의 추가 기능 수행.
        //    BaseChildForm BCH = tabMyTab.SelectedTab.Controls[0] as BaseChildForm;
        //    BCH.DoNew();
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    // 다형성을 통한 자식 클래스의 삭제 기능 수행.
        //    BaseChildForm BCH = tabMyTab.SelectedTab.Controls[0] as BaseChildForm;
        //    BCH.DoDelete();
        //}

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    // 다형성을 통한 자식 클래스의 저장 기능 수행.
        //    BaseChildForm BCH = tabMyTab.SelectedTab.Controls[0] as BaseChildForm;
        //    BCH.DoSave();
        //}

        #endregion

        #region < 툴바의 공통 기능 구현 소스 > 
        // 조회 / 추가 / 삭제 / 저장 버튼이 공통으로 수행할 메서드.
        // 메서드 가 구현되는 틀, 방식 (인수의 개수, 데이터 타입, 반환형식 ) = 메서드 의 시그니쳐
        // 조회 / 추가 / 삭제 / 저장 버튼 이 동일한 메서드의 시그니쳐 를 가지고 있기 때문에 
        // 아래의 로직이 가능하다. 
        private void efgierlgkllkherg(object sender_, EventArgs ea)
        {
            // 버튼의 종류 구분하기 . 
            ToolStripButton TSB = sender_ as ToolStripButton;

            // 조회 , 추가 , 삭제 , 저장 버튼에 따른 기능 수행하기 로직. 
            BaseChildForm BCH = tabMyTab.SelectedTab.Controls[0] as BaseChildForm;

            switch (TSB.Tag.ToString())
            {
                case "SEARCH":
                    BCH.DoInquire();
                    break;
                case "NEW":
                    BCH.DoNew();
                    break;
                case "DELETE":
                    BCH.DoDelete();
                    break;
                case "SAVE":
                    BCH.DoSave();
                break;
            } 
        }
        #endregion
    }
}

