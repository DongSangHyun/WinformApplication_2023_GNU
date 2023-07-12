using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MianForms
{
    public partial class M01_LogIn : Form
    {
        public M01_LogIn()
        {
            InitializeComponent();
        }

        

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            // 로그인 버튼 을 클릭 했을때 진입점. 

            // 벨리데이션 체크 
            // . 개발자가 프로그램 로직을 수행하는 코딩을 하기 전에 
            //    예외적으로 검출해야 하는 상황 또는 정상 로직이 수행 되지 않는 조건 을 
            //    미리 예측하고 코딩에 적용하는 방법

            string sUserId   = string.Empty;   // 사용자 ID  변수 등록
            string sPassword = string.Empty;   // 비밀번호   변수 등록

            // 사용자 ID 와 PW 를 정확히 입력 하였는지 확인
            sUserId   = txtUserId.Text;   // 사용자 ID 변수에 사용자ID 텍스트 박스에 입력한 내용 할당
            sPassword = txtPassword.Text; // 사용자 비밀번호 변후 세 사용자 비밀번호 텍스트박스에 입력한 내용 할당.

            // 필수 입력 사항이 누락 되었을 경우 변수. 
            string Message = string.Empty;
            if (sUserId == "")
            {
                Message = "사용자 ID ";
            }
            else if (sPassword == "")
            {
                Message += "비밀번호 ";
            }

            if (Message != "")
            {
                MessageBox.Show($"{Message} 를 입력 하지 않았습니다.");
                return;
            }

            // 1. 데이터 베이스 접속 경로 .     
            string Sconnection = "Server = DESKTOP-Q580OO3\\MSSQLSERVER01; Uid = sa; Pwd = 1234; database = AppDev;";

            // 2. 데이터 베이스 접속 할수 있는 클래스 
            SqlConnection Connect = new SqlConnection(Sconnection);

            // 정상 데이터베이스 접속
            try
            {
                // 3. 데이터 베이스 open
                Connect.Open();

                // 4. 데이터 베이스에 전달할 명령 
                string sSqlSelect = $"SELECT * FROM TB_USER WHERE USERID = '{sUserId}' AND PW = '{sPassword}'";

                // 5. 명령을 전달 할 클래스
                SqlDataAdapter adapter = new SqlDataAdapter(sSqlSelect, Connect);

                // 6. 데이터 베이스 조회 수행 및 결과 반환. 
                // DataTable : C# 에서 데이터 베이스의 반환 결과 를 할당 받을 수 있는 자료구조. 
                DataTable dTtemp = new DataTable();
                adapter.Fill(dTtemp); 

                // 로그인 을 할 수 있는 사용자 인지 체크 
                if (dTtemp.Rows.Count == 0)
                {
                    MessageBox.Show("사용자 ID 또는 비밀번호가 잘못되었습니다.");
                    txtUserId.Text = "";   // 사용자 ID
                    txtPassword.Text = ""; // 사용자 비밀번호 
                    txtUserId.Focus(); // 커서 위치 를 활성화
                    return;
                }

                // 정상 로그인 처리 로직.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // 데이터 베이스 접속 종료
                Connect.Close(); 
            }

        }
    }
}
