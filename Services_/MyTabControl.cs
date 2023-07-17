using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_
{
    // 사용자 정의 컨트롤 
    public class MyTabControl : TabControl
    {
        // 클래스의 상속
        // 생성 되어있는 클래스의 기능을 그대로 물려받고 
        // 새로운 기능을 추가 할 수 있다. 

        // TabControl 의 기능을 모두 상속 받아. 
        // TabPage 에 호출된 화면을 추가하여 표현하는 기능을 추가. 
        
        /// <summary>
        /// 윈폼 클래스를 인자로 받아 탭페이지에 표현하는 메서드 입니다.
        /// </summary>
        /// <param name="NewForm"> 텝 페이지로 표현한 윈폼 클래스 입니다. </param>
        public void AddForm(Form NewForm)
        {
            if (NewForm == null) return;  // 인자로 받은 폼 이 null 처리 되어있을 경우 실행 중지. 
            NewForm.TopLevel = false;     // 컨트롤에 페이지 에 첫 페이지로 등록 되지 않음 

            TabPage page = new TabPage(); // 탭 컨트롤에 추가 할 페이지.

            page.Controls.Clear();      // 새로 만든 페이지 를 초기화 . 
            page.Controls.Add(NewForm); // 페이지 에 표현 하려고 하는 Form  을 추가. 
            page.Text = NewForm.Text;   // 페이지 의 제목 은 추가하려고 하는 Form 의 Text 를 표현. 
            page.Name = NewForm.Name;   // 페이지 의 Name 은 추가 Form 의 Name 을 등록.

            
            base.TabPages.Add(page);    // 생성 된 탭 페이지 를 탭 컨트롤에 추가. 
            NewForm.Show();             // 추가되는 Form 클래스 를 표현. 
            base.SelectedTab = page;    // 추가한 페이지를 활성화.
        }
    }
}
