﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;


namespace B0700_FastSocket_TelnetServer
{
    /// <summary>
    /// 退出命令
    /// </summary>
    public sealed class ExitCommand : ICommand<StringCommandInfo>
    {
        /// <summary>
        /// 返回命令名称
        /// </summary>
        public string Name
        {
            get { return "exit"; }
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandInfo"></param>
        public void ExecuteCommand(IConnection connection, StringCommandInfo commandInfo)
        {
            connection.BeginDisconnect();//断开连接
        }
    }
}
