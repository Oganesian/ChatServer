﻿using ChatClient.Models;
using System;

namespace ChatClient.ViewModels
{
    public class UserControlMessageViewModel : BindableBase
    {
        private readonly UserControlMessageModel model;

        private string _text;
        private string _timestamp;

        public UserControlMessageViewModel(string text, DateTime timestamp)
        {
            model = new UserControlMessageModel();
            Text = text;
            Timestamp = timestamp.ToString("t");
        }

        public string Text
        {
            get
            {
                return model.Text;
            }
            set
            {
                model.Text = value;
                SetProperty(ref _text, value);
            }
        }

        public string Timestamp
        {
            get
            {
                return model.Timestamp;
            }
            set
            {
                model.Timestamp = value;
                SetProperty(ref _timestamp, value);
            }
        }
    }
}
