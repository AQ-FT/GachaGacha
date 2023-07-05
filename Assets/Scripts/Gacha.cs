using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NCMB;

public class Gacha : MonoBehaviour
{
    /* プライベート系のフィールド */
    [SerializeField] GameObject selectPanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject popupPanel;
    [SerializeField] GameObject moneyaddPanel; // ほんとは別画面読み込んだ方がよさそうだけどめんどいのでパネルつかう
    protected int money_Gacha = 0; // 現在の所持金のフィールド
    private GachaTable[] gachatable = new GachaTable[3];
    private System.Random random;
   

    /* 公開系のフィールド */
    public TextMeshProUGUI money; // 金額表示用
    public TextMeshProUGUI money_main; // 金額表示用
    public TextMeshProUGUI result; // 結果表示用

    /* 定数定義 */
    const int value = 250; // ガチャの値段
    const int value_tenth = 2500; // 10連ガチャの値段
    const int maxIndex = 3; // レアリティの個数（Normal、Rare、SuperRareで３つ）
    const int countPerPack = 10; // 1回に引ける個数

    void Start()
    {
        // 抽選リストをロード
        for (int i = 0 ; i < maxIndex ; i++) {
            gachatable[i] =  Resources.Load<GachaTable>("GachaTable/" + (i + 1).ToString());
        }
        random = new System.Random((int)DateTime.Now.Ticks);

        /*パネルなどの初期化*/
        money.SetText(money_Gacha.ToString());
        money_main.SetText(money_Gacha.ToString());
        CloseButton();
    }

    public List<string> packOpen(int money)
    {
        List<string> resultList = new List<string>();
        int totalProbability = 0;
        
        
        for (int i = 0 ; i < maxIndex ; i++) 
        {
            // レアリティの確率を足し合わせる
            totalProbability += gachatable[i].probability;
        }
        
        resultList = new List<string>(); // 抽選結果格納用変数
        for (int i = 0 ; i < countPerPack ; i++) 
        {
　　　　　　 // 抽選を行う
            string card = getCard(totalProbability);
            resultList.Add(card);    
        }

        return resultList;
    }

    private string getCard(int _allProbability)
    {
        // ガチャ全体の確率を足し合わせた数値から乱数を作成 (レアリティの抽選)
        int randomValue = getRamdom(_allProbability);
        int totalProbability = 0;

        for (int i = 0 ; i < maxIndex ; i++) 
        {
            totalProbability += gachatable[i].probability;
            if(totalProbability >= randomValue) 
            {
　　　　　　　　　// そのレアに含まれるキャラ数から乱数を作成 (キャラの抽選)
                string id = getRamdom(gachatable[i].cards);
                return id;
            }
        }
        return null;
    }

    private int getRamdom(int _max)
    {
        return random.Next(0, _max);
    }

    private string getRamdom(List<string> _list)
    {
        return _list[random.Next(0, _list.Count)];
    }

    public void OnceGachaOpen()
    {
        if(money_Gacha < value)
        {
            popupPanel.SetActive(true);
　　　　　　 // お金が足りなかったらnullを返却
            return;
        }

        List<string> GachaResult = packOpen(money_Gacha);
        if(GachaResult == null){
            return;
        }

        /* パネルを設定 */
        selectPanel.SetActive(false);
        moneyaddPanel.SetActive(false);
        popupPanel.SetActive(false);
        resultPanel.SetActive(true);

        money_Gacha -= 250;
        money.SetText(money_Gacha.ToString());
        money_main.SetText(money_Gacha.ToString());
        
        result.SetText(GachaResult[0]);

        NCMBObject SendClass = new NCMBObject("SendClass");
        SendClass["message"] = GachaResult[0];
        SendClass.SaveAsync();
    }

    public void TenthGachaOpen()
    {
        if(money_Gacha < value_tenth)
        {
            popupPanel.SetActive(true);
　　　　　　 // お金が足りなかったらnullを返却
            return;
        }

        List<string> GachaResult = packOpen(money_Gacha);
        if(GachaResult == null){
            return;
        }

        /* パネルを設定 */
        selectPanel.SetActive(false);
        moneyaddPanel.SetActive(false);
        popupPanel.SetActive(false);
        resultPanel.SetActive(true);

        money_Gacha -= 2500;
        money.SetText(money_Gacha.ToString());
        money_main.SetText(money_Gacha.ToString());

        result.SetText(GachaResult[0]+ "\n" +GachaResult[1]+ "\n" +GachaResult[2]+ "\n" +GachaResult[3]+ "\n" +GachaResult[4]+ "\n" +GachaResult[5]+ "\n" +GachaResult[6]+ "\n" +GachaResult[7]+ "\n" +GachaResult[8]+ "\n" +GachaResult[9]);

        for(int i=0; i<10; i++) 
        {
            NCMBObject SendClass = new NCMBObject("SendClass");
            SendClass["message"] = GachaResult[i];
            SendClass.SaveAsync();
        }
    }

    /* 金額追加の関数群 */
    public void INGacha_money_500() {
        money_Gacha += 500;
        money.SetText(money_Gacha.ToString());
        money_main.SetText(money_Gacha.ToString());
    }

    public void INGacha_money_1000() {
        money_Gacha += 1000;
        money.SetText(money_Gacha.ToString());
        money_main.SetText(money_Gacha.ToString());
    }

    public void INGacha_money_10000() {
        money_Gacha += 10000;
        money.SetText(money_Gacha.ToString());
        money_main.SetText(money_Gacha.ToString());
    }

    public void INGacha_money_PanelSetta()
    {
        /* パネル変更で画面遷移 */
        selectPanel.SetActive(false);
        moneyaddPanel.SetActive(true);
        resultPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void Closepopup() // ポップアップ用のパネルをオフ
    {
        popupPanel.SetActive(false);
    }

    public void CloseButton() //パネルのオンオフ
    {
        selectPanel.SetActive(true);
        moneyaddPanel.SetActive(false);
        resultPanel.SetActive(false);
        popupPanel.SetActive(false);
    }
}
