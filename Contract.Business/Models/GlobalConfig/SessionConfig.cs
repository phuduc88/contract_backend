﻿using System;

namespace Contract.Business.Models
{
    public class SessionConfig
    {
        #region Session Configs

        public string LoginSecretKey { get; set; }
        public TimeSpan SessionTimeout { get; set; }
        public bool CreateTokenRandom { get; set; }
        public TimeSpan SessionResetPasswordExpire { get; set; }

        public string FolderAsset { get; set; }

        #endregion
    }
}
