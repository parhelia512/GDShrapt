﻿namespace GDShrapt.Reader
{
    public class GDFigureCloseBracket : GDSingleCharToken, IGDStructureToken
    {
        public override char Char => '}';
    }
}