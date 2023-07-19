using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_
{

    // 추상 화 클래스 
    // 부모 클래스 에서 자식 클래스가 구현 해야할 기능을 추상적으로 구현 해 두고 
    // 부모 클래스를 상속 받은 자식클래스 가 부모의 기능을 반드시 구현 해야 하거나 ( Abstract , Override )
    // 선택 적으로 구현 (Virtual , Override) 하게끔 유도하는 방식 .
    //  - 1. 자식클래스 에서 반드시 구현해야 하는 기능의 누락 을 방지 할 수 있다.
    //    2. 프로젝트에서 구현 해야하는 기능의 명칭 을 통일 할 수 있다.
    public partial class BaseChildForm : Form
    {
        public BaseChildForm()
        {
            InitializeComponent();
        }

        // 상속을 받은 클래스에서 반드시 이 명칭으로 기능을 구현해야 함을 강제.
        public virtual void DoInquire()
        {

        }
        // 조회 하는 기능은 이 이름으로 구현 할것. 

        // 상속을 받은 클래스 에서 구현 을 선택할수 있게 하는 추상화 기능.
        public virtual void DoSave()
        {

        }

        public virtual void DoNew()
        {
            // 툴바의 추가 버튼을 클릭 했을 때 상속받는 클래스가 공통으로 구현해야 하는 메서드
        }

        public virtual void DoDelete()
        {
            // 툴바의 삭제 버튼을 클릭 했을 때 상속받는 클래스가 공통으로 구현해야 하는 메서드
        }
    }
}
