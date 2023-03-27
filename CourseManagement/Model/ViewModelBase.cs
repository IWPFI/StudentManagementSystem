using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManagementSystem
{
    public class ViewModelBase : NotifyBase
    {
        //private CommandBase _closeCommand;
        ///// <summary>
        ///// 关闭命令
        ///// </summary>
        //public CommandBase CmdWindowClose
        //{
        //    get
        //    {
        //        _closeCommand = new CommandBase();
        //        _closeCommand.DoCanExecute = new Func<object, bool>((obj) => true);
        //        _closeCommand.DoExecute = new Action<object>((obj) =>
        //        {
        //            (obj as Window).Close();
        //        });
        //        return _closeCommand;
        //    }
        //}

        //private CommandBase _dragMoveCommand;
        ///// <summary>
        ///// 拖动窗口命令
        ///// </summary>
        //public CommandBase CmdWindowDragMove
        //{
        //    get
        //    {
        //        _dragMoveCommand = new CommandBase();
        //        _dragMoveCommand.DoCanExecute = new Func<object, bool>((o) => true);
        //        _dragMoveCommand.DoExecute = new Action<object>((o) =>
        //        {
        //            (o as Window).DragMove();
        //        });
        //        return _dragMoveCommand;
        //    }
        //}

        #region 命令[Command]
        /// <summary>
        /// 窗口拖动命令
        /// </summary>
        public ICommand CmdWindowDragMove => new RelayCommand<object>((o) => { (o as Window).DragMove(); });

        /// <summary>
        /// 窗口关闭命令
        /// </summary>
        public ICommand CmdWindowClose => new RelayCommand<object>((o) => { (o as Window).Close(); });

        /// <summary>
        /// 窗口最小化命令
        /// </summary>
        public ICommand CmdWindowMinimized => new RelayCommand<object>((obj) =>
        {
            (obj as Window).WindowState = WindowState.Minimized;
        });

        /// <summary>
        /// 窗口状态改变命令
        /// </summary>
        /// <remarks>
        /// 最大化/正常
        /// </remarks>
        public ICommand CmdWindowStateChange => new RelayCommand<object>((obj) =>
        {
            (obj as Window).WindowState = (obj as Window).WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        });
        #endregion
    }
}

