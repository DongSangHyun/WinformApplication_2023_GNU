using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services_;

namespace MianForms
{
    public partial class M02_PasswordChange : Form
    {
        public M02_PasswordChange()
        {
            InitializeComponent();
        }

        private void btnPWChange_Click(object sender, EventArgs e)
        {
            // 비밀 번호 변경 버튼을 클릭

            string sUserid   = txtUserId.Text;   // 입력한 사용자 ID
            string sNowPw    = txtNowPW.Text;    // 입력한 현재 비밀번호
            string sChangePW = txtChangePW.Text; // 입력한 변경 할 비밀번호

            #region < 벨리데이션 체크. >
            // string.Empty : "" 을 있어 보이게 하는 명령어 
            string sMessage = string.Empty; // 누락 값 존재 시 입력 될 에러 메세지.
            if (sUserid == "") sMessage        += "아이디 ";
            else if (sNowPw == "") sMessage    += "현재 비밀번호 ";
            else if (sChangePW == "") sMessage += "변경 할 비밀번호 ";

            if (sMessage != string.Empty)
            {
                MessageBox.Show($"{sMessage} 를 입력하지 않았습니다.");
                return;
            }
            #endregion

            #region < 비밀번호 변경을 할수 있는 ID 와 PW 인지 확인. >

            // 1. 데이터 베이스 주소  
            string Sconnection = Commons.Sconnection;

            // 2. 데이터 베이스 오픈. 
            SqlConnection connection  = new SqlConnection(Sconnection);

            try
            {
                connection.Open();

                // 3. 데이터 베이스에 전달 할 명령. 
                string sSqlSelect = $"SELECT * FROM TB_USER WHERE USERID = '{sUserid}' AND PW = '{sNowPw}'";

                // 4. 데이터 베이스 전달 객체에 설정. 
                SqlDataAdapter adapter = new SqlDataAdapter(sSqlSelect, connection);

                // 5. 데이터베이스 에 실행 및 결과 반환. 
                DataTable dtTemp = new DataTable();
                adapter.Fill(dtTemp);

                // 6. 결과 값이 존재 하지 않을 경우 비밀번호 변경 실패. 
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("사용자 아이디 와 비밀번호 가 일치하지 않습니다.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // 데이터 베이스 닫기.
                connection.Close();
            }
            #endregion

            #region < 비밀 번호 변경 로직 적용 >
            // 변경 내역 적용 여부 확인하기.
            DialogResult result = MessageBox.Show("비밀번호 변경 내역을 저장 하시겠습니까?", "비밀번호 변경", MessageBoxButtons.YesNo);
            if (result == DialogResult.Cancel) return;
            // 위의 코드는 아래로 줄여서 표현 할 수 있다.
            //if (MessageBox.Show("비밀번호 변경 내역을 저장 하시겠습니까?", "비밀번호 변경", MessageBoxButtons.YesNo) == DialogResult.Cancel) return;
            
            try
            {
                connection.Open();

                // 1. 데이터 베이스의 데이터를 갱신(추가, 삭제, 수정) 을 할수 있는 클래스.
                SqlCommand cmd = new SqlCommand();

                // 2. 커맨드 객체가 실행 해야 할 데이터 베이스 주소.
                cmd.Connection = connection;

                // 3. SQL 명령 문 작성. 
                string sSqlUpdate = $" UPDATE TB_USER                      " +
                                    $"    SET PW       = '{sChangePW}',    " +
                                    $"        EDITDATE = '{string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)}'  " +
                                    $"  WHERE USERID   = '{sUserid}'       ";

                // 4. SQL 명령 문 커맨드에 등록. 
                cmd.CommandText = sSqlUpdate;

                // 5. 명령 실행. 
                cmd.ExecuteNonQuery();

                MessageBox.Show("정상적으로 비밀번호 를 변경 하였습니다.");

                // 현재 화면 (비밀번호 변경) 을 닫기 
                //this.Visible = false; // 메모리에 계속 상주 해 있으나 눈에만 보이지 않는다. 
                this.Close(); // 비밀 번호 변경 클래스 에서 더이상 실행 할 로직이 없을 경우 화면을 종료(모달(ShowDialog) 상태를 해제)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            { 
                connection.Close(); 
            }

            #endregion 
        }
    }
}
