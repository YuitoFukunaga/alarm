using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alarm_rb {
    public partial class Form1:Form {
        public Form1() {
            InitializeComponent();
        }

        DateTime now = DateTime.Now;//現在時刻を秒間隔で格納する変数
        SoundPlayer soundPlayer = new SoundPlayer("kaisi.wav");//時間になったら鳴らす音

        /*
         * 
         * 起動時設定
         *
         */

        /*
         * 0 Sun 
         * 1 Mon 
         * 2 Tue 
         * 3 Wed
         * 4 Thu
         * 5 Fri
         * 6 Sat
         */

        int day_of_week;

        /*set_alarm ={
        *   {時間,コースの種類}
        *}
        *
        *コースの種類
        *0 : テラス,スプリンギン,プログラミング開始or終了
        *1 : テラス,スプリンギン開始or終了
        *2 : プログラミング開始or終了
        *-1 : なし
        */

        int[,] set_startalarm = {
            { 1600,0},{ 1710,1},{ 1740,2},{ 1820,1}
        };

        int[,] set_endalarm = {
                {1700,1},{1730,2 },{1810,1 },{1910,2},{1920,1}
        };
        //1７３０
        String[] alarm_message = { "テラス,スプリンギン,プログラミング","テラス,スプリンギン","プログラミング"};


        /*
         * 
         * 起動時設定ここまで
         *
         */
        
        //秒を合わせるためのflag
        Boolean second_flag = false;
        //アラームを実行する曜日か判断するためのflag
        Boolean day_of_week_alarm_flag = false;
        //アラーム時間になっているかのflag
        Boolean alarm_flag = false;

        //起動時実行される関数
        private void Form1_Load(object sender,EventArgs e) {
            time.Text=now.ToString("HH:mm");//時刻を表示
            day_of_week = ((int)(now.DayOfWeek));//曜日を設定
            if(day_of_week == 1 || day_of_week==2 || day_of_week==3 || day_of_week==5){//アラームが必要な曜日か
                day_of_week_alarm_flag = true;
            }
            timer1.Enabled=true;//タイマー起動
            timer1.Interval=1000;//タイマー間隔を1秒に設定
        }

        //設定されている間隔で実行
        private void timer1_Tick(object sender,EventArgs e) {
            now=DateTime.Now;
            //秒→分間隔にするための処理
            if(!second_flag) {
                if(int.Parse(now.ToString("ss")) == 0 ){
                    timer1.Interval = 60000;//1分間隔に設定
                    second_flag = true;
                }
            }
            if(day_of_week_alarm_flag) {
                for(int i = 0;i<set_startalarm.GetLength(1);i++) {
                    if(int.Parse(now.ToString("HHmm"))==set_startalarm[i,0]) {
                        label1.Text=alarm_message[set_startalarm[i,1]]+"開始";
                        alarm_flag=true;
                        soundPlayer.SoundLocation="kaisi.wav";
                    }
                }
                for(int i = 0;i<set_endalarm.GetLength(1);i++) {
                    if(int.Parse(now.ToString("HHmm"))==set_endalarm[i,0]) {
                        label1.Text=alarm_message[set_endalarm[i,1]]+"終了";
                        alarm_flag=true;
                        soundPlayer.SoundLocation="syuuryou.wav";
                    }
                }

                if(alarm_flag) {
                    soundPlayer.Play();
                    alarm_flag=false;
                }
            }
            time.Text=now.ToString("HH:mm");
        }

        private void button1_Click(object sender,EventArgs e) {
            soundPlayer.SoundLocation="kaisi.wav";
            soundPlayer.Play();
        }
    }
}
