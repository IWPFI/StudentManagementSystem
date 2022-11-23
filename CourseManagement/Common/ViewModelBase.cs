using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManagementSystem.Common
{
    public class ViewModelBase : NotifyBase
    {
        private CommandBase _closeCommand;
        /// <summary>
        /// 关闭命令
        /// </summary>
        public CommandBase CloseCommand
        {
            get
            {
                _closeCommand = new CommandBase();
                _closeCommand.DoCanExecute = new Func<object, bool>((obj) => true);
                _closeCommand.DoExecute = new Action<object>((obj) =>
                {
                    (obj as Window).Close();
                });
                return _closeCommand;
            }
        }

        private CommandBase _dragMoveCommand;
        /// <summary>
        /// 拖动窗口命令
        /// </summary>
        public CommandBase DragMoveCommand
        {
            get
            {
                _dragMoveCommand = new CommandBase();
                _dragMoveCommand.DoCanExecute = new Func<object, bool>((o) => true);
                _dragMoveCommand.DoExecute = new Action<object>((o) =>
                {
                    (o as Window).DragMove();
                });
                return _dragMoveCommand;
            }
        }
    }
}

