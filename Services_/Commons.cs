using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 서비스 로직 , 비즈니스 로직 (딴)
// 프로그램에 직접적인 영향을 주지 않는 대신 시스템 운영에 필요한 정보 들을 모아두고 사용 할 수 있는 모듈

namespace Services_
{
    // 클래스 라이브러리, 프로젝트 파일, DLL, 네임스페이스,  어셈블리 
    // 하나 이상의 앱(프로젝트) 에서  호출되는 형식 및 메서드 등을 정의하여 DLL로 재공. 
    // 외부 프로젝트 에서 DLL 파일을 참조 함으로서 DLL 에 있는 클래스의 기능을 사용 할 수 
    // 있도록 한다. (재사용성 , 유지 보수성) . 


    public class Commons
    {
        // Commons 
        // 시스템 에서 사용 하는 공통 변수 또는 메서드 등의 모음. 

        // static
        // 응용 프로그램 실행 시 최초 1회 초기화 할 때 Data 영역에 등록이 되어 
        // 응용프로그램 이 종료 될 때 까지 상주 해 있도록 하는 키워드

        // const 
        // 변하지 않는 상수. 
        // 기본적으로 static 의 성격을 띄고있다. 
        public const string Sconnection = "Server = DESKTOP-Q580OO3\\MSSQLSERVER01; Uid = sa; Pwd = 1234; database = AppDev;";

        public static bool bLoginFlag = false; // 로그인의 성공여부를 가지고있을 변수.
    }
}
