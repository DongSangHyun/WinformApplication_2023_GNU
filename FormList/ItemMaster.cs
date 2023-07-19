using Services_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormList
{
    public partial class ItemMaster : BaseChildForm
    {
        // 클래스 내에서 전역으로 사용할 데이터 테이블 (참조)
        DataTable dtTemp = new DataTable();

        // 데이터 베이스 접속 
        SqlConnection sCon = new SqlConnection(Commons.Sconnection);

        public ItemMaster()
        {
            InitializeComponent();
        }

        #region < Form Load > 
        private void ItemMaster_Load(object sender, EventArgs e)
        {
            // 그리드 셋팅 
            // 그리드 : 행과 열의 데이터 베이스 테이블의 데이터를 사용자가 확인하고 관리 할 수 있도록
            //          제공되는 디자인 기능을 가지고 있는 컨트롤 (클래스)

            // 1. 그리드에 셋팅이 될 데이터 테이블 의 컬럼 설정. 
            dtTemp.Columns.Add("ITEMCODE", typeof(string)); // 품목 코드
            dtTemp.Columns.Add("ITEMNAME", typeof(string)); // 폼목 명
            dtTemp.Columns.Add("ITEMTYPE", typeof(string)); // 품목 유형
            dtTemp.Columns.Add("ITEMDESC", typeof(string)); // 품목 상세
            dtTemp.Columns.Add("ENDFLAG", typeof(string)); // 단종여부
            dtTemp.Columns.Add("PRODDATE", typeof(string)); // 출시 일자
            dtTemp.Columns.Add("MAKEDATE", typeof(string)); // 품목의 정보를 등록한 일시
            dtTemp.Columns.Add("MAKER", typeof(string)); // 등록자
            dtTemp.Columns.Add("EDITDATE", typeof(string)); // 수정일시
            dtTemp.Columns.Add("EDITOR", typeof(string)); // 수정자.

            // 2. 컬럼이 설정된 데이터 테이블 을 그리드뷰 에 셋팅 (매핑) 
            // DataSource :DataTable 의 내용을 기반으로 그리드 에 행과 열의 디자인을 표현하는 속성
            Grid.DataSource = dtTemp;

            // 3. 그리드에 표현될 헤더(컬럼의 제목) 의 명칭을 설정. 
            Grid.Columns["ITEMCODE"].HeaderText = "폼목 코드";
            Grid.Columns["ITEMNAME"].HeaderText = "폼목 명";
            Grid.Columns["ITEMTYPE"].HeaderText = "품목 유형";
            Grid.Columns["ITEMDESC"].HeaderText = "품목 상세";
            Grid.Columns["ENDFLAG"].HeaderText = "단종여부";
            Grid.Columns["PRODDATE"].HeaderText = "출시 일자";
            Grid.Columns["MAKEDATE"].HeaderText = "등록 일시";
            Grid.Columns["MAKER"].HeaderText = "등록자";
            Grid.Columns["EDITDATE"].HeaderText = "수정일시";
            Grid.Columns["EDITOR"].HeaderText = "수정자";

            // 컬럼의 폭 지정
            Grid.Columns["ITEMCODE"].Width = 100;
            Grid.Columns[1].Width = 200;
            Grid.Columns[2].Width = 200;
            Grid.Columns[3].Width = 200;
            Grid.Columns[4].Width = 100;


            // 컬럼의 숨김 여부 지정 
            Grid.Columns["ITEMDESC"].Visible = false;


            // 컬럼의 정렬 지정 
            Grid.Columns["ENDFLAG"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //컬럼의 수정 여부를 지정
            Grid.Columns["ITEMCODE"].ReadOnly = true;
            Grid.Columns["MAKER"].ReadOnly = true;
            Grid.Columns["MAKEDATE"].ReadOnly = true;
            Grid.Columns["EDITDATE"].ReadOnly = true;
            Grid.Columns["EDITOR"].ReadOnly = true;




            // 콤보박스 셋팅 
            // 특정한 데이터를 리스트에 등록해 두고 사용자가 선택하여 사용할 수 있도록 
            // 해 주는 기능이 있는 디자인 컨트롤  (클래스)
            // 데이터 베이스에 등록 되어있는 품목의 유형을 콤보박스에 셋팅 하고 사용자가 선택하도록 유도. 


            try
            {
                // 데이터 베이스 오픈. 
                sCon.Open();

                string sSelectSql = " SELECT MINORCODE  AS CODE_ID,  " +
                                    "        CODENAME   AS CODE_NAME " +
                                    "   FROM TB_Standard             " +
                                    "  WHERE MAJORCODE = 'ITEMTYPE'  " +
                                    "    AND MINORCODE<> '$'         ";

                // 데이터 베이스를 조회 하는 클래스
                SqlDataAdapter adapter = new SqlDataAdapter(sSelectSql, sCon);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 콤보박스에 셋팅되어야 하는 데이터 를 매핑. 
                cboItemType.DataSource = dt;

                // 응용프로그램에서 사용할 코드 와 사용자 가 눈으로 확인할 Text 속성을 분류
                cboItemType.ValueMember = "CODE_ID";
                cboItemType.DisplayMember = "CODE_NAME";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sCon.Close();
            }
        }
        #endregion

        #region < ToolBar F > 
        public override void DoInquire()
        {
            // 품목을 조회 할 때 수행하는 기능.  
            try
            {
                sCon.Open();

                // 그리드 데이터 초기화 
                dtTemp.Clear();

                // 조회 조건의 데이 를 변수에 담기 . 
                string sItemCode = txtItemCode.Text; // 사용자가 입력한 품목 코드 
                string sItemName = txtItemName.Text; // 사용자가 입력한 품목 명
                string sStart = dtpStart.Text;    // 사용자가 입력한 출시 시작 일자 .
                string sEnd = dtpEnd.Text;      // 사용자가 입력한 출시 종료 일자 범위
                string sItemType = cboItemType.SelectedValue as string; // 선택한 콤보박스(품목 유형) 코드값.
                string sItemNameOnly = chkNameOnly.Checked == true ? "Y" : "N";
                string sEndFalg = rdoProduct.Checked == true ? "N" : "Y"; // 생산 / 단종 여부.

                // 조회 해 올 데이터베이스에 전달 할 명령문 작성.
                string sSqlSelect = " SELECT *                                        " +
                                    "   FROM TB_ItemMaster                            " +
                                    $" WHERE ITEMCODE LIKE '%{sItemCode}%'                 " +
                                    $"   AND ITEMNAME LIKE '%{sItemName}%'                 " +
                                    $"   AND PRODDATE BETWEEN '{sStart}' AND '{sEnd}' " +
                                    $"   AND ITEMTYPE = '{sItemType}'                 " +
                                    $"   AND ENDFLAG  = '{sEndFalg}'                  ";

                SqlDataAdapter adapter = new SqlDataAdapter(sSqlSelect, sCon);
                adapter.Fill(dtTemp);

                // 데이터 베이스에서 받아온 결과 를 그리드에 표현
                Grid.DataSource = dtTemp;

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
            finally
            {
                sCon.Close();
            }
        }

        public override void DoNew()
        {
            // 그리드와 바인딩 ( 주소를 서로 참조하고있는 관계 ) 된 데이터 테이블에 
            // 새로운 행을 추가 함으로서 그리드에 행을 추가 하는 로직. 
            dtTemp.Rows.Add();
        }

        public override void DoDelete()
        {
            // 그리드에 데이터 행을 삭제 처리 (데이터베이스 에서 처리 전) 
            if (Grid.Rows.Count == 0) return; // 그리드에 삭제 할 내역이 존재 하지 않을경우 return;
            if (Grid.CurrentRow == null)
            {
                MessageBox.Show("삭제 할 대상을 선택하세요.");
                return; // 삭제할 대상 행을 선택하지 않았을때. return;
            }

            // 삭제 할 품목 데이터 의 품목코드 정보.
            string sItemCode = Grid.CurrentRow.Cells["ITEMCODE"].Value.ToString();

            // 데이터 테이블에서 해당 품목 코드 를 가지고 있는 행을 삭제. 
            // i : 데이터 테이블의 행의 Index
            for (int i = 0;i < dtTemp.Rows.Count; i++)
            {
                // 데이터 테이블에서 삭제할 대상 품목 코드 를 찾았다면. 
                if (dtTemp.Rows[i].RowState != DataRowState.Deleted && dtTemp.Rows[i]["ITEMCODE"].ToString() == sItemCode)
                {
                    // 해당 데이터 테이블의 행을 삭제
                    dtTemp.Rows[i].Delete();
                    break;
                }
            }
        }


        public override void DoSave()
        {
            base.DoSave();

        }
        #endregion 

    }
}
