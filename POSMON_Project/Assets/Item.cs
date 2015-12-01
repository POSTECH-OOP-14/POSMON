using UnityEngine;
using System.Collections;

public enum item_no
{
    SKILL1 = 100, SKILL2, SKILL3, SKILL4, SKILL5, SKILL6, SKILL7, SKILL8, SKILL9,
    CURE1 = 0, CURE2, CURE3, CURE4, CURE5, CURE6, CURE7, CURE8, CURE9, CURE10, CURE11, 
    CAPTURE1 = 200, CAPTURE2, CAPTURE3, CAPTURE4, CAPTURE5, CAPTURE6, CAPTURE7, CAPTURE8, CAPTURE9,
    QUEST1 = 300, QUEST2, QUEST3, QUEST4, QUEST5, QUEST6, QUEST7, QUEST8, QUEST9
};

public enum status { paralysis, sleep, poison, fire, none, faint, all };

public enum type { skill, cure, capture, quest };

public class Item
{
    private int index;//아이템 번호
    private bool battle;//전투 중에 사용가능한 아이템이면 true, 아니면 false
    private int price;//아이템 가격
    private string name;//아이템 이름
    private double amount;//회복 아이템이 회복시키는 HP
    private status st;//회복 아이템이 회복시키는 상태
    private type type;//아이템 종류
    private double possibility;//포획 아이템 성공률
    private string info;//아이템 설명
    private SkillInfo skillinfo;//기술머신의 스킬

    public Item(item_no n)
    {
        switch (n)
        {
            case item_no.SKILL1:
                {
                    name = "효율적인 안면강타를 위한 조언";
                    index = (int)n;
                    price = 2800;
                    battle = false;
                    type = type.skill;
                    info = "안면강타 기술을 배웁니다.";
                    info = info + "\n\n 과에 상관없이 배울 수 있습니다.";
                    skillinfo = new SkillInfo(23,name,30,0,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10);
                    break;
                }
            case item_no.SKILL2:
                {
                    name = "상대방을 멘붕시키는 씨뿌리기";
                    index = (int)n;
                    price = 3400;
                    battle = false;
                    type = type.skill;
                    info = "씨뿌리기 기술을 배웁니다.";
                    info = info + "\n\n상태이상 마비를 일으킬 수 있습니다.";
                    skillinfo = new SkillInfo(24, name, 10, 0, 1, new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, status.paralysis, 10, 10);
                    break;
                }
            case item_no.SKILL3:
                {
                    name = "자라나라, 날개여!";
                    index = (int)n;
                    price = 4600;
                    battle = false;
                    type = type.skill;
                    info = "날개펼치기 기술을 배웁니다.";
                    info = info + "\n\n날개를 펼쳐 술을 마시게 만듭니다.";
                    skillinfo = new SkillInfo(25, name, 40, 0, 0, new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, status.none, 5, 5);
                    break;
                }
            case item_no.SKILL4:
                {
                    name = "배부르고 나른한 오후2시의 수업";
                    index = (int)n;
                    price = 2400;
                    battle = false;
                    type = type.skill;
                    st = status.sleep;
                    info = "오후2시수업 기술을 배웁니다.";
                    info = info + "\n\n상태이상 잠을 일으킵니다.";
                    skillinfo = new SkillInfo(26, name, 20, 0, 1, new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, status.sleep, 10, 10);
                    break;
                }
            case item_no.SKILL5:
                {
                    name = "알쏭달쏭 수학퀴즈";
                    index = (int)n;
                    price = 2400;
                    battle = false;
                    type = type.skill;
                    info = "수학퀴즈 기술을 배웁니다.";
                    skillinfo = new SkillInfo(27, name, 20, 0, 0, new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, status.none, 10, 10);
                    break;
                }
            case item_no.CURE1:
                {
                    name = "나무열매";
                    index = (int)n;
                    price = 500;
                    amount = 15;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    info = "폭풍의 언덕의 나무에서 직접 채취한 나무열매! 향긋향긋~ ";
                    info = info + "\n\n HP를 15 회복시킵니다.";
                    break;
                }
            case item_no.CURE2:
                {
                    name = "콜라";
                    index = (int)n;
                    price = 1200;
                    amount = 30;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    info = "탄산은 역시 콜라! 꿀꺾꿀꺾 끄어어어억";
                    info = info + "\n\n HP를 30 회복시킵니다.";
                    break;
                }
            case item_no.CURE3:
                {
                    name = "몬스터 드링크";
                    index = (int)n;
                    price = 2000;
                    amount = 50;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    info = "공대생의 필수품! 에너지 드링크!";
                    info = info + "\n\n HP를 50 회복시킵니다.";
                    break;
                }
            case item_no.CURE4:
                {
                    name = "학식 2500";
                    index = (int)n;
                    price = 2500;
                    amount = 0.25;
                    st = status.none;
                    battle = true;
                    type = type.cure;
                    info = "먹어도 배가 고픈 학식! 냠냠~";
                    info = info + "\n\n HP를 1/4 회복시킵니다.";
                    break;
                }
            case item_no.CURE5:
                {
                    name = "학식 4000";
                    index = (int)n;
                    price = 4000;
                    amount = 0.5;
                    st = status.none;
                    battle = true;
                    type = type.cure;
                    info = "가끔씩은 기분좋게 학식 D코너를 먹어요";
                    info = info + "\n\n HP를 1/2 회복시킵니다.";
                    break;
                }
            case item_no.CURE6:
                {
                    name = "연지 1일 사용권";
                    index = (int)n;
                    price = 6000;
                    amount = 1.00;
                    st = status.none;
                    battle = true;
                    type = type.cure;
                    info = "맛있지만 비싼 연지를 먹어요.";
                    info = info + "\n\n HP를 모두 회복시킵니다.";
                    break;
                }
            case item_no.CURE7:
                {
                    name = "초코에몽";
                    index = (int)n;
                    price = 1700;
                    amount = 0;
                    st = status.paralysis;
                    battle = true;
                    type = type.cure;
                    info = "포스텍 학생들의 영원한 친구 초코에몽>.<";
                    info = info + "\n\n 상태이상 마비를 회복시킵니다.";
                    break;
                }
            case item_no.CURE8:
                {
                    name = "오독오독 제과";
                    index = (int)n;
                    price = 2100;
                    amount = 0;
                    st = status.poison;
                    battle = true;
                    type = type.cure;
                    info = "오독오독 씹히는 맛이 있는 오독오독 제과";
                    info = info + "\n\n 상태이상 독을 회복시킵니다.";
                    break;
                }
            case item_no.CURE9:
                {
                    name = "드라이아이스";
                    index = (int)n;
                    price = 800;
                    amount = 0;
                    st = status.fire;
                    battle = true;
                    type = type.cure;
                    info = "고체에서 기체로 승화하는 드라이아이스";
                    info = info + "\n\n 상태이상 화상을 회복시킵니다.";
                    break;
                }
            case item_no.CURE10:
                {
                    name = "생명의 조각";
                    index = (int)n;
                    price = 9900;
                    amount = 0;
                    st = status.faint;
                    battle = true;
                    type = type.cure;
                    info = "아름답고 신비로운 생명의 조각";
                    info = info + "\n\n 상태이상 기절을 회복시킵니다.";
                    break;
                }
            case item_no.CURE11:
                {
                    name = "불꽃의 싸다구";
                    index = (int)n;
                    price = 2300;
                    amount = 0;
                    st = status.sleep;
                    battle = true;
                    type = type.cure;
                    info = "맞으면 정신이 번쩍 드는 불꽃의 싸다구";
                    info = info + "\n\n 상태이상 잠을 회복시킵니다.";
                    break;
                }
            case item_no.CAPTURE1:
                {
                    name = "학부생 프로젝트 제안";
                    index = (int)n;
                    price = 300;
                    battle = true;
                    type = type.capture;
                    possibility = 1.0;
                    info = "자네, 이거 한번 해보지 않겠나?";
                    break;
                }
            case item_no.CAPTURE2:
                {
                    name = "1년 지도회식";
                    index = (int)n;
                    price = 700;
                    battle = true;
                    type = type.capture;
                    possibility = 1.5;
                    info = "1년동안 교수님의 돈으로 맛있는 음식을 팡팡!";
                    break;
                }
            case item_no.CAPTURE3:
                {
                    name = "군대 면제증";
                    index = (int)n;
                    price = 2100;
                    battle = true;
                    type = type.capture;
                    possibility = 2;
                    info = "군대를 안갈수만 있다면 무엇이든 하겠어";
                    break;
                }
            case item_no.CAPTURE4:
                {
                    name = "대학원 입학서";
                    index = (int)n;
                    price = 1600;
                    battle = true;
                    type = type.capture;
                    possibility = 3;
                    info = "포스텍 대학원에 합격했다!! 야호!!";
                    break;
                }
            case item_no.CAPTURE5:
                {
                    name = "포스텍 다니는 여자친구";
                    index = (int)n;
                    price = 3800;
                    battle = true;
                    type = type.capture;
                    possibility = 5.0;
                    info = "어머낫, 포스텍에 여자친구가 생겼다!";
                    info = info + "\n\n어쩔 수 없이 포스텍 대학원에 가야겠군 ><";
                    break;
                }
            case item_no.CAPTURE6:
                {
                    name = "URP 프로그램";
                    index = (int)n;
                    price = 900;
                    battle = true;
                    type = type.capture;
                    possibility = 2.5;
                    info = "돈도 받으면서 연구를 할 수 있다고? 개이득!";
                    break;
                }
            case item_no.CAPTURE7:
                {
                    name = "노벨상 후보";
                    index = (int)n;
                    price = 3000;
                    battle = true;
                    type = type.capture;
                    possibility = 4.5;
                    info = "에헴에헴 나는야 노벨상 후보 교수라네";
                    info = info + "나와 함께 노벨상을 받아보지 않겠나?";
                    break;
                }

        }

    }

    public SkillInfo getSkillInfo()
    {
        return skillinfo;
    }

    public string getInfo()
    {
        return info;
    }

    public double getPossibility()
    {
        return possibility;
    }

    public int getPrice()
    {
        return price;
    }

    public string getName()
    {
        return name;

    }
    public type getType()
    {
        return type;
    }
    public double getAmount()
    {
        return amount;
    }
    public status getStatus()
    {
        return st;
    }

    public void setAmount(double n)
    {
        amount = n;
    }

    public void addAmount()
    {
        amount++;
    }

    public bool useItem( Student s)
    {
        amount--;
        if (this.getType() == type.skill)
        {
            return s.addSkill(this);
        }
        else if (this.getType() == type.cure)
        {
            return s.changeStatus(this);
        }
        else if (this.getType() == type.capture)
        {
            return s.capture(this);
        }
        return false;
    }

    public int getIndex()
    {
        return index;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
