﻿using System;
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
using Services_;

namespace MianForms
{
    public partial class M01_LogIn : Form
    {
        public M01_LogIn()
        {
            InitializeComponent();
        }

        

        private void btnLogIn_Click(object sender = null, EventArgs e = null)
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
            string Sconnection = Commons.Sconnection;

            // 2. 데이터 베이스 접속 할수 있는 클래스 
            SqlConnection Connect = new SqlConnection(Sconnection);

            // 정상 데이터베이스 접속
            try
            {
                // 3. 데이터 베이스 open
                Connect.Open();

                #region < 사용자 ID 와 PW 가 동시에 일치하는 경우 로 로그인 여부 확인 > 
                //// 4. 데이터 베이스에 전달할 명령 
                //string sSqlSelect = $"SELECT * FROM TB_USER WHERE USERID = '{sUserId}' AND PW = '{sPassword}'";

                //// 5. 명령을 전달 할 클래스
                //SqlDataAdapter adapter = new SqlDataAdapter(sSqlSelect, Connect);

                //// 6. 데이터 베이스 조회 수행 및 결과 반환. 
                //// DataTable : C# 에서 데이터 베이스의 반환 결과 를 할당 받을 수 있는 자료구조. 
                //DataTable dTtemp = new DataTable();
                //adapter.Fill(dTtemp); 

                //// 로그인 을 할 수 있는 사용자 인지 체크 
                //if (dTtemp.Rows.Count == 0)
                //{
                //    MessageBox.Show("사용자 ID 또는 비밀번호가 잘못되었습니다.");
                //    txtUserId.Text   = "";   // 사용자 ID
                //    txtPassword.Text = ""; // 사용자 비밀번호 
                //    txtUserId.Focus(); // 커서 위치 를 활성화
                //    return;
                //}

                //string sDbUserId = dTtemp.Rows[0][0].ToString();  // 데이터 베이스 에서 받아온 사용자의 ID

                #endregion

                #region < T_ 로그인 할 수 없을 경우 사용자 ID 가 없는 경우인지, ID 는 일치하는데 비밀번호가 맞지 않는지 에 대한 메세지처리 및 로그인 미처리 >
                //4. SQL 전달 명령 작성. 
                
                // 입력한 ID 와 동일한 데이터가 존재 할 경우 결과 행이 반환
                string sSqlSelect = $"SELECT * FROM TB_USER WHERE USERID = '{sUserId}'";

                // 5. 데이터 베이스 실행 객체 등록. 
                SqlDataAdapter adapter = new SqlDataAdapter(sSqlSelect, Connect);

                // 6. 데이터 베이스 에서 결과(테이블 형태)를 받아올 그릇
                DataTable dtTemp = new DataTable(); // 참조

                // 7. 명령을 실행하고 결과를 받아오기. 
                adapter.Fill(dtTemp);

                // 8. 데이터테이블이 결과 값이 없다 ? ( 등록된 ID 가 없다. )
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("사용자 ID 가 존재 하지 않습니다.");
                    txtUserId.Text   = "";   // 사용자 ID
                    txtPassword.Text = "";   // 사용자 비밀번호 
                    txtUserId.Focus();       // 커서 위치 를 활성화
                    return;
                }
                else if (sPassword != dtTemp.Rows[0]["PW"].ToString())
                {
                    MessageBox.Show("비밀번호 가 일치하지 않습니다.");
                    txtPassword.Text = "";   // 사용자 비밀번호 
                    txtPassword.Focus();     // 커서 위치 를 활성화
                    return;
                }

                // 정상 로그인 처리 로직.

                MessageBox.Show($"{dtTemp.Rows[0]["USERNAME"]} 님 반갑습니다.");
                #endregion 
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // KeyEventArgs : 이벤트 메서드 를 실행 할때 던져주는 이벤트에 대한 속성 (키의 정보)
            // 키보드의 키를 입력 할때 실행되는 메서드.
            if (e.KeyCode == Keys.Enter)
            {
                // 로그인 기능을 수행. 
                btnLogIn_Click();
            }
        }

        private void btnPWChange_Click(object sender, EventArgs e)
        {
            // 비밀 번호 변경 버튼 을 클릭 했을 때 이벤트
            M02_PasswordChange M02 = new M02_PasswordChange();
            //M02.Show(); // 비동기식 윈폼 클래스 창 호출. 
            // 모달창,동기식으로 비밀번호 변경 창 호출
            // = 비밀번호변경 창이 닫히기 전까지 는 해당 메서드의 로직을 수행 하지 않고 기다린다. 

            // 로그인 창을 잠시 숨기기. 
            this.Visible = false;

            M02.ShowDialog();
            this.Visible = true;
            MessageBox.Show("화면이 닫혔습니다.");
        }
    }
}
