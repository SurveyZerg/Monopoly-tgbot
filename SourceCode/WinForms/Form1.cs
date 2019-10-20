﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Monopoly_tgbot
{
    public partial class Form1 : Form
    {
        public static TelegramBotClient Client;
        const string token = "928805208:AAFqHquYSpuNQxCj7RORd7TzGyTXpsHm44E";       
        public Form1()
        {
            InitializeComponent();
            Client = new TelegramBotClient(token);
            Client.OnMessage += MessageHandlerAsync;
        }


        static private async void MessageHandlerAsync(object sender, MessageEventArgs args)
        {
            if (sender is TelegramBotClient && args.Message.Text != null)
            {
                await Client.SendTextMessageAsync(args.Message.Chat.Id, "hey b0ss");
                //тут обработка сообщений
            }
        }
        //Button click handle
        private void StartStop_Click(object sender, EventArgs e)
        {
            if (Client.IsReceiving)
                Stop();
            else
                Start();
        }

        void Stop()
        { 
            Client.StopReceiving();
            StartStop.Text = "Start";
            AddText("Bot stopped");
        }
        void Start()
        {
            
            Client.StartReceiving();
            StartStop.Text = "Stop";
            AddText("Bot started");
        }

        delegate void SetTextCallback(string text);

        private void AddText(string text)
        {
            if (this.Log.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.Log.Text += text+"\n";
            }
        }
    }
}