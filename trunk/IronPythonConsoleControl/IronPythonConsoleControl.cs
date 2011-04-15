﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Scripting.Hosting.Shell;
using System.IO;
using System.Drawing;

namespace IronPythonConsoleControl
{
    /// <summary>
    /// The IronPythonConsoleControl is a reusable control for implementing the IConsole
    /// interface used by IronPython.
    /// </summary>
    public class IronPythonConsoleControl: Control, IConsole
    {
        private TextWriter _stderr;
        private TextWriter _stdout;
        private List<OutputLine> _logicalLines; // the actual output, before rendering
        private OutputLine _currentLine; // the line we are currently writing to (this is the last line in _lines)
        private Font _font = new Font("Courrier New", 12);

        public IronPythonConsoleControl(): base()
        {
            DoubleBuffered = true;
            _logicalLines = new List<OutputLine>();
            _currentLine = new OutputLine();
            _logicalLines.Add(_currentLine);
        }                

        /// <summary>
        /// Update the output to reflect the text written so far.
        /// First, we figure out what the whole text looks like, then
        /// which portion we can display.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            var yPosition = 0;
            foreach (var line in _logicalLines)
            {
                line.Print(graphics, yPosition);
                yPosition += line.GetHeight(graphics, Width);
            }
        }

#region IConsole memebers
        public TextWriter ErrorOutput { get { return _stderr; } set { _stderr = value; } }
        public TextWriter Output { get { return _stdout; } set { _stdout = value; } }

        /// <summary>
        /// Blocks until the user has entered a line in the console.
        /// </summary>
        public string ReadLine(int autoIndentSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write some text to the output. We need to take care to
        /// remember the lines, so we can reproduce the text later on.
        /// 
        /// For now, we just ignore "style".
        /// </summary>
        public void Write(string text, Style style)
        {
            var lines = text.Split(new string[]{Environment.NewLine}, StringSplitOptions.None);
            if (lines.Length == 1)
            {
                // no newlines in text
                _currentLine.Append(new OutputSpan(lines[0], _font));
            }
            else
            {
                foreach (var line in lines.Take(lines.Length - 1)) // we will treat the last line specially
                {
                    _currentLine.Append(new OutputSpan(line, _font));                    
                    _currentLine = new OutputLine(); // start a new line
                    _logicalLines.Add(_currentLine);
                }
                var lastline = lines.Last();
                if (lastline != Environment.NewLine)
                {
                    // reuse logic for no newlines in text
                    Write(lastline, style);
                }
                // else: already handled in foreach: _lines.Add(_currentLine)
            }
            Invalidate();            
        }

        public void WriteLine(string text, Style style)
        {            
            Write(text + Environment.NewLine, style);
        }

        public void WriteLine()
        {
            Write(Environment.NewLine, Style.Out);
        }
 
#endregion IConsole members

        
    }
}
