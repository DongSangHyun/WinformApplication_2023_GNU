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
    public abstract partial class BaseChildForm : Form
    {
        public BaseChildForm()
        {
            InitializeComponent();
        }

        // 상속을 받은 클래스에서 반드시 이 명칭으로 기능을 구현해야 함을 강제.
        public abstract void DoInquire(); // 조회 하는 기능은 이 이름으로 구현 할것. 

        // 상속을 받은 클래스 에서 구현 을 선택할수 있게 하는 추상화 기능.
        public virtual void DoSave()
        { 

        }
    }
}
