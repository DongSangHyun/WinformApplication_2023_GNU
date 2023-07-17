using Services_;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MianForms
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main() // 주 진입점. 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //메인 클래스 가 실행 되기 전에 Login 클래스 를 실행. 
            //M01_LogIn login = new M01_LogIn();
            //login.ShowDialog();

            // 어떻게 로그인 클래스 에서 
            // program 클래스 로. 로그인 성공 여부의 신호를 보낼수 있을까?

            #region < 정적 변수 를 이용한 메인 화면 호출 방법 > 
            //if (Commons.bLoginFlag == true)
            //{
            //    // 로그인이 성공을 했을때에 
            //    // 아래의 메인 창을 오픈 해야 한다..
            //    Application.Run(new M03_MainForm());
            //}
            #endregion

            #region < 클래스 변수 를 이용한 로그인 여부 확인 방법 >
            //if (login.bLoginFlag)
            //{
            //    Application.Run(new M03_MainForm());
            //}
            #endregion

            #region < Tag 속성을 이용한 로그인 여부 확인 방법 > 
            //ArrayList List = login.Tag as  ArrayList;
            //if (List != null && (bool)List[0] == true)
            {
                //Application.Run(new M03_MainForm(List[1].ToString()));
                Application.Run(new M03_MainForm("관리자"));
            }
            #endregion 
        }
    }
}
