using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIControl
{
    public partial class CommandBase : MonoBehaviour
    {
        public abstract class CommandCursorMove
        {
            public abstract void CursorMove(CommandBase owner, Vector2 dir);
        }
        public class NoneMove : CommandCursorMove
        {
            public override void CursorMove(CommandBase owner, Vector2 dir) { }
        }
        public class MoveUDInR : CommandCursorMove
        {
            public override void CursorMove(CommandBase owner, Vector2 dir)
            {
                if (dir.x > 0)
                {
                    owner.Decide();
                    return;
                }
                if (dir.y < 0)
                {
                    owner.NextCommand();
                }
                else
                {
                    owner.BackCommand();
                }
            }
        }
        public class MoveUDOnly : CommandCursorMove
        {
            public override void CursorMove(CommandBase owner, Vector2 dir)
            {
                if (dir.y < 0)
                {
                    owner.NextCommand();
                }
                else
                {
                    owner.BackCommand();
                }
            }
        }
        public class MoveLRInD : CommandCursorMove
        {
            public override void CursorMove(CommandBase owner, Vector2 dir)
            {
                if (dir.y < 0)
                {
                    owner.Decide();
                    return;
                }
                if (dir.x > 0)
                {
                    owner.NextCommand();
                }
                else
                {
                    owner.BackCommand();
                }
            }
        }
        public class MoveLRInDOutU : CommandCursorMove
        {
            public override void CursorMove(CommandBase owner, Vector2 dir)
            {
                if (dir.y < 0)
                {
                    owner.Decide();
                    return;
                }
                else if (dir.y > 0)
                {
                    owner.Cancel();
                    return;
                }
                if (dir.x > 0)
                {
                    owner.NextCommand();
                }
                else
                {
                    owner.BackCommand();
                }
            }
        }
        public class MoveLROutUD : CommandCursorMove
        {
            public override void CursorMove(CommandBase owner, Vector2 dir)
            {
                if (dir.y != 0)
                {
                    owner.Cancel();
                    return;
                }
                if (dir.x > 0)
                {
                    owner.NextCommand();
                }
                else
                {
                    owner.BackCommand();
                }
            }
        }
    }
}
