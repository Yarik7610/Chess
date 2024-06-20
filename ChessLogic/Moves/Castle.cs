﻿namespace ChessLogic
{
    public class Castle : Move
    {
        public override MoveType Type { get; }
        public override Position FromPos { get; } //for king
        public override Position ToPos { get; } //for king

        private Direction kingMoveDir;
        private Position rookFromPos;
        private Position rookToPos;

        public Castle(MoveType type, Position kingPos, Board board)
        {
            Type = type;
            FromPos = kingPos;
            if (!board.isRotated)
            {
                if (type == MoveType.CastleKS)
                {
                    kingMoveDir = Direction.East;
                    ToPos = new Position(kingPos.Row, 6);
                    rookFromPos = new Position(kingPos.Row, 7);
                    rookToPos = new Position(kingPos.Row, 5);
                }
                else if (type == MoveType.CastleQS)
                {
                    kingMoveDir = Direction.West;
                    ToPos = new Position(kingPos.Row, 2);
                    rookFromPos = new Position(kingPos.Row, 0);
                    rookToPos = new Position(kingPos.Row, 3);
                }
            }
            else 
            {
                if (type == MoveType.CastleKS)
                {
                    kingMoveDir = Direction.West;
                    ToPos = new Position(kingPos.Row, 1);
                    rookFromPos = new Position(kingPos.Row, 0);
                    rookToPos = new Position(kingPos.Row, 2);
                }
                else if (type == MoveType.CastleQS)
                {
                    kingMoveDir = Direction.East;
                    ToPos = new Position(kingPos.Row, 5);
                    rookFromPos = new Position(kingPos.Row, 7);
                    rookToPos = new Position(kingPos.Row, 4);
                }
            }

        }
        public override bool Execute(Board board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);
            return false;
        }
        public override bool IsLegal(Board board)
        {
            Player player = board[FromPos].Color;
            if (board.IsInCheck(player)) return false;
            Board copy = board.Copy();
            Position kingPosInCopy = FromPos;

            for (int i = 0; i < 2; i++)
            {
                new NormalMove(kingPosInCopy, kingPosInCopy + kingMoveDir).Execute(copy);
                kingPosInCopy += kingMoveDir;

                if (copy.IsInCheck(player)) return false;
            }
            return true;
        }
    }
}
