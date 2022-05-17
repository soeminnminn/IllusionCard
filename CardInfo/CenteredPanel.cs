using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel.Design;

namespace S16.Windows.Forms
{
    [Designer(typeof(CenteredPanelDesigner)), Docking(DockingBehavior.AutoDock), ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class CenteredPanel : Control
    {
        #region Native Methods
        private const int WS_HSCROLL = 0x100000;
        private const int WS_VSCROLL = 0x200000;

        private const int SIF_RANGE = 0x1;
        private const int SIF_PAGE = 0x2;
        private const int SIF_POS = 0x4;
        private const int SIF_DISABLENOSCROLL = 0x8;
        private const int SIF_TRACKPOS = 0x10;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;

        private const int SB_BOTTOM = 7;
        private const int SB_ENDSCROLL = 8;
        private const int SB_LEFT = 6;
        private const int SB_LINEDOWN = 1;
        private const int SB_LINELEFT = 0;
        private const int SB_LINERIGHT = 1;
        private const int SB_LINEUP = 0;
        private const int SB_PAGEDOWN = 3;
        private const int SB_PAGELEFT = 2;
        private const int SB_PAGERIGHT = 3;
        private const int SB_PAGEUP = 2;
        private const int SB_RIGHT = 7;
        private const int SB_THUMBPOSITION = 4;
        private const int SB_THUMBTRACK = 5;
        private const int SB_TOP = 6;

        // ms-help://MS.MSDNQTR.v90.en/shellcc/platform/commctls/scrollbars/scrollbarreference/scrollbarmessages/wm_hscroll.htm
        private const int WM_HSCROLL = 0x114;
        // ms-help://MS.MSDNQTR.v90.en/shellcc/platform/commctls/scrollbars/scrollbarreference/scrollbarmessages/wm_vscroll.htm
        private const int WM_VSCROLL = 0x115;

        // ms-help://MS.MSDNQTR.v90.en/shellcc/platform/commctls/scrollbars/scrollbarreference/scrollbarstructures/scrollinfo.htm
        [StructLayout(LayoutKind.Sequential)]
        internal class SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;

            public SCROLLINFO()
            {
                this.cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
            }

            public SCROLLINFO(int mask, int min, int max, int page, int pos)
            {
                this.cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
                this.fMask = mask;
                this.nMin = min;
                this.nMax = max;
                this.nPage = page;
                this.nPos = pos;
            }
        }

        // ms-help://MS.MSDNQTR.v90.en/gdi/rectangl_6cqa.htm
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern int SetScrollInfo(HandleRef hWnd, int fnBar, SCROLLINFO lpcScrollInfo, bool fRedraw);

        [DllImport("user32.dll")]
        private static extern int ScrollWindow(HandleRef hWnd, int XAmount, int YAmount, RECT lpRect, RECT lpClipRect);

        // ms-help://MS.MSDNQTR.v90.en/gdi/pantdraw_4zef.htm
        [DllImport("user32.dll")]
        private static extern int UpdateWindow(HandleRef hwnd);

        private static int HIWORD(int n)
        {
            return ((n >> 0x10) & 0xffff);
        }

        private static int LOWORD(int n)
        {
            return (n & 0xffff);
        }

        #endregion

        #region Variables
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private const string interiorPageName = "interiorPage";

        private CenteredPanelPage interiorPage;

        private bool m_hScrollVisible = false;
        private bool m_vScrollVisible = false;

        private int m_xCurrentScroll = 0;
        private int m_xMaxScroll = 0;
        private int m_yCurrentScroll = 0;
        private int m_yMaxScroll = 0;
        #endregion

        #region Constructor
        public CenteredPanel()
            : base()
        {
            this.InitializeControl();
        }
        #endregion

        #region Initialize Control
        private void InitializeControl()
        {
            this.CreateControls();
            //base.AutoScroll = true;
        }

        private void ChangeStyles()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
        }

        private void CreateControls()
        {
            this.interiorPage = new CenteredPanelPage(this);

            this.SuspendLayout();
            //
            // m_InteriorPage
            //
            this.interiorPage.Location = new Point(0, 0);
            this.interiorPage.Size = new Size(100, 100);
            this.interiorPage.Name = interiorPageName;

            this.Controls.Add(this.interiorPage);

            this.ResumeLayout(false);
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x10000;
                if (this.m_hScrollVisible) createParams.Style |= WS_HSCROLL;
                if (this.m_vScrollVisible) createParams.Style |= WS_VSCROLL;
                return createParams;
            }
        }
        #endregion

        #region Override Methods
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.DesignMode)
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, this.ClientRectangle);
            }

            this.DrawCaption(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            if (!this.DesignMode) this.ResizeContainer();
            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //e.Delta
            base.OnMouseWheel(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            //base.OnGotFocus(e);
            this.interiorPage.Focus();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case CenteredPanel.WM_HSCROLL:
                    this.WmHScroll(ref m);
                    break;

                case CenteredPanel.WM_VSCROLL:
                    this.WmVScroll(ref m);
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Protected Methods
        protected void WmHScroll(ref Message m)
        {
            int newPos = this.m_xCurrentScroll;
            ScrollEventType type = ScrollEventType.First;

            switch (CenteredPanel.LOWORD((int)m.WParam))
            {
                case CenteredPanel.SB_ENDSCROLL:
                    // Ends scroll.
                    type = ScrollEventType.EndScroll;
                    break;

                case CenteredPanel.SB_LEFT:
                    // Scrolls to the upper left.
                    type = ScrollEventType.First;
                    newPos = 0;
                    break;

                case CenteredPanel.SB_RIGHT:
                    // Scrolls to the lower right.
                    type = ScrollEventType.Last;
                    newPos = this.m_xMaxScroll;
                    break;

                case CenteredPanel.SB_LINELEFT:
                    // Scrolls left by one unit.
                    type = ScrollEventType.SmallDecrement;
                    newPos = this.m_xCurrentScroll - 5;
                    break;

                case CenteredPanel.SB_LINERIGHT:
                    // Scrolls right by one unit.
                    type = ScrollEventType.SmallIncrement;
                    newPos = this.m_xCurrentScroll + 5;
                    break;

                case CenteredPanel.SB_PAGELEFT:
                    // Scrolls left by the width of the window.
                    type = ScrollEventType.LargeDecrement;
                    newPos = this.m_xCurrentScroll - 50;
                    break;

                case CenteredPanel.SB_PAGERIGHT:
                    // Scrolls right by the width of the window.
                    type = ScrollEventType.LargeIncrement;
                    newPos = this.m_xCurrentScroll + 50;
                    break;

                case CenteredPanel.SB_THUMBPOSITION:
                    // The user has dragged the scroll box (thumb) and released the mouse button. The high-order word indicates the position of the scroll box at the end of the drag operation.
                    type = ScrollEventType.ThumbPosition;
                    newPos = CenteredPanel.HIWORD((int)m.WParam);
                    break;

                case CenteredPanel.SB_THUMBTRACK:
                    // The user is dragging the scroll box. This message is sent repeatedly until the user releases the mouse button. The high-order word indicates the position that the scroll box has been dragged to.
                    type = ScrollEventType.ThumbTrack;
                    break;

            }

            newPos = Math.Max(0, newPos);
            newPos = Math.Min(this.m_xMaxScroll, newPos);

            if (newPos == this.m_xCurrentScroll) return;

            int delta = newPos - this.m_xCurrentScroll;

            if (newPos > ((this.m_xMaxScroll - this.Width) + 20)) return;

            Console.WriteLine(string.Format("HScroll newPos = {0} delta = {1}", newPos, delta));

            this.m_xCurrentScroll = newPos;

            CenteredPanel.ScrollWindow(this.HandleRefrence, -delta, 0, new RECT(), new RECT());
            CenteredPanel.SetScrollInfo(this.HandleRefrence, CenteredPanel.SB_HORZ, new SCROLLINFO(CenteredPanel.SIF_POS, 0, 0, 0, newPos), true);
            CenteredPanel.UpdateWindow(this.HandleRefrence);

            ScrollEventArgs e = new ScrollEventArgs(type, newPos, ScrollOrientation.HorizontalScroll);
            this.OnScroll(e);
        }

        protected void WmVScroll(ref Message m)
        {
            int newPos = this.m_yCurrentScroll;

            ScrollEventType type = ScrollEventType.First;
            switch (LOWORD((int)m.WParam))
            {
                case SB_ENDSCROLL:
                    // Ends scroll.
                    type = ScrollEventType.EndScroll;
                    break;

                case SB_TOP:
                    // Scrolls to the upper left.
                    type = ScrollEventType.First;
                    newPos = 0;
                    break;

                case SB_BOTTOM:
                    // Scrolls to the lower right.
                    type = ScrollEventType.Last;
                    newPos = this.m_yMaxScroll;
                    break;

                case SB_LINEUP:
                    // Scrolls one line up.
                    type = ScrollEventType.SmallDecrement;
                    newPos = this.m_yCurrentScroll - 5;
                    break;

                case SB_LINEDOWN:
                    // Scrolls one line down.
                    type = ScrollEventType.SmallIncrement;
                    newPos = this.m_yCurrentScroll + 5;
                    break;

                case SB_PAGEUP:
                    // Scrolls one page up.
                    type = ScrollEventType.LargeDecrement;
                    newPos = this.m_yCurrentScroll - 50;
                    break;

                case SB_PAGEDOWN:
                    // Scrolls one page down.
                    type = ScrollEventType.LargeIncrement;
                    newPos = this.m_yCurrentScroll + 50;
                    break;

                case SB_THUMBPOSITION:
                    // The user has dragged the scroll box (thumb) and released the mouse button. The high-order word indicates the position of the scroll box at the end of the drag operation.
                    type = ScrollEventType.ThumbPosition;
                    newPos = HIWORD((int)m.WParam);
                    break;

                case SB_THUMBTRACK:
                    // The user is dragging the scroll box. This message is sent repeatedly until the user releases the mouse button. The high-order word indicates the position that the scroll box has been dragged to.
                    type = ScrollEventType.ThumbTrack;
                    break;
            }

            newPos = Math.Max(0, newPos);
            newPos = Math.Min(this.m_yMaxScroll, newPos);

            if (newPos == this.m_yCurrentScroll) return;
            if (newPos > ((this.m_yMaxScroll - this.Height) + 20)) return;

            int delta = newPos - this.m_yCurrentScroll;

            Console.WriteLine(string.Format("VScroll newPos = {0} delta = {1}", newPos, delta));

            this.m_yCurrentScroll = newPos;

            ScrollWindow(this.HandleRefrence, 0, -delta, new RECT(), new RECT());
            SetScrollInfo(this.HandleRefrence, SB_VERT, new SCROLLINFO(SIF_POS, 0, 0, 0, newPos), true);
            UpdateWindow(this.HandleRefrence);

            ScrollEventArgs e = new ScrollEventArgs(type, newPos, ScrollOrientation.VerticalScroll);
            this.OnScroll(e);
        }

        protected virtual void OnScroll(ScrollEventArgs e)
        {
        }
        #endregion

        #region Private Methods
        private void DrawCaption(Graphics g)
        {
            //g.FillRectangle(new SolidBrush(Color.Aqua), new Rectangle(0, 0, this.Width, this.headerSize));
        }

        private void DrawFocus(Graphics g, Rectangle r)
        {
            r.Inflate(-1, -1);
            ControlPaint.DrawFocusRectangle(g, r, this.ForeColor, this.BackColor);
        }

        private void ResizeContainer()
        {
            this.interiorPage.SuspendLayout();

            int x = (this.Width - this.interiorPage.Width) / 2;
            int y = (this.Height - this.interiorPage.Height) / 2;

            this.m_xCurrentScroll = 0;
            this.m_yCurrentScroll = 0;

            this.m_xMaxScroll = this.interiorPage.Width;
            this.m_yMaxScroll = this.interiorPage.Height;

            try
            {
                if (this.Width < this.interiorPage.Width)
                {
                    x = 0;

                    if (!this.m_hScrollVisible)
                    {
                        this.m_hScrollVisible = true;
                        this.RecreateHandle();
                    }
                }
                else
                {
                    if (this.m_hScrollVisible)
                    {
                        this.m_hScrollVisible = false;
                        this.RecreateHandle();
                    }
                }

                if (this.Height < this.interiorPage.Height)
                {
                    y = 0;

                    if (!this.m_vScrollVisible)
                    {
                        this.m_vScrollVisible = true;
                        this.RecreateHandle();
                    }
                }
                else
                {
                    if (this.m_vScrollVisible)
                    {
                        this.m_vScrollVisible = false;
                        this.RecreateHandle();
                    }
                }

                if (this.m_hScrollVisible)
                {
                    CenteredPanel.SetScrollInfo(this.HandleRefrence, SB_HORZ, new SCROLLINFO(SIF_RANGE | SIF_PAGE | SIF_POS, 0, this.m_xMaxScroll, this.Width, 0), true);
                }

                if (this.m_vScrollVisible)
                {
                    CenteredPanel.SetScrollInfo(this.HandleRefrence, SB_VERT, new SCROLLINFO(SIF_RANGE | SIF_PAGE | SIF_POS, 0, this.m_yMaxScroll, this.Height, 0), true);
                }
            }
            catch (System.Exception)
            {
            }

            this.interiorPage.Location = new Point(x, y);

            this.interiorPage.ResumeLayout();
        }
        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), Description("InteriorPage"), Localizable(false)]
        public CenteredPanelPage InteriorPage
        {
            get { return this.interiorPage; }
        }

        private HandleRef HandleRefrence
        {
            get { return new HandleRef(this, this.Handle); }
        }

        //[DefaultValue(true)]
        //public override bool AutoScroll
        //{
        //    get
        //    {
        //        return base.AutoScroll;
        //    }
        //    set
        //    {
        //        base.AutoScroll = value;
        //    }
        //}
        #endregion

        #region Nested Types
        [Designer(typeof(PageDesigner)), ClassInterface(ClassInterfaceType.AutoDispatch), ToolboxItem(false), DesignTimeVisible(false), DefaultEvent("Click")]
        public class CenteredPanelPage : Panel
        {
            #region Variables
            /// <summary> 
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;
            private CenteredPanel m_Owner;
            #endregion

            #region Events
            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public new event EventHandler DockChanged
            {
                add
                {
                    base.DockChanged += value;
                }
                remove
                {
                    base.DockChanged -= value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new event EventHandler EnabledChanged
            {
                add
                {
                    base.EnabledChanged += value;
                }
                remove
                {
                    base.EnabledChanged -= value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public new event EventHandler LocationChanged
            {
                add
                {
                    base.LocationChanged += value;
                }
                remove
                {
                    base.LocationChanged -= value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new event EventHandler TabIndexChanged
            {
                add
                {
                    base.TabIndexChanged += value;
                }
                remove
                {
                    base.TabIndexChanged -= value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new event EventHandler TabStopChanged
            {
                add
                {
                    base.TabStopChanged += value;
                }
                remove
                {
                    base.TabStopChanged -= value;
                }
            }

            [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
            public new event EventHandler TextChanged
            {
                add
                {
                    base.TextChanged += value;
                }
                remove
                {
                    base.TextChanged -= value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new event EventHandler VisibleChanged
            {
                add
                {
                    base.VisibleChanged += value;
                }
                remove
                {
                    base.VisibleChanged -= value;
                }
            }
            #endregion

            #region Constructor
            public CenteredPanelPage(CenteredPanel owner)
                : base()
            {
                this.InitializeControl();
                this.m_Owner = owner;
                base.SetStyle(ControlStyles.ResizeRedraw, true);
            }
            #endregion

            #region Initialize Control
            private void InitializeControl()
            {
            }
            #endregion

            #region Override Methods
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            #endregion

            #region Private Methods
            #endregion

            #region Public Methods
            #endregion

            #region Properties
            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public override AnchorStyles Anchor
            {
                get
                {
                    return base.Anchor;
                }
                set
                {
                    base.Anchor = value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override bool AutoSize
            {
                get
                {
                    return base.AutoSize;
                }
                set
                {
                    base.AutoSize = value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Localizable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override AutoSizeMode AutoSizeMode
            {
                get
                {
                    return AutoSizeMode.GrowOnly;
                }
                set
                {
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public override DockStyle Dock
            {
                get
                {
                    return base.Dock;
                }
                set
                {
                    base.Dock = value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public new ScrollableControl.DockPaddingEdges DockPadding
            {
                get
                {
                    return base.DockPadding;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new bool Enabled
            {
                get
                {
                    return base.Enabled;
                }
                set
                {
                    base.Enabled = value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new Point Location
            {
                get
                {
                    return base.Location;
                }
                set
                {
                    base.Location = value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(typeof(Size), "0, 0")]
            public override Size MaximumSize
            {
                get
                {
                    return base.MaximumSize;
                }
                set
                {
                    base.MaximumSize = value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public override Size MinimumSize
            {
                get
                {
                    return base.MinimumSize;
                }
                set
                {
                    base.MinimumSize = value;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public new Control Parent
            {
                get
                {
                    return base.Parent;
                }
                set
                {
                    base.Parent = value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new Size PreferredSize
            {
                get
                {
                    return base.PreferredSize;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new int TabIndex
            {
                get
                {
                    return base.TabIndex;
                }
                set
                {
                    base.TabIndex = value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public new bool TabStop
            {
                get
                {
                    return base.TabStop;
                }
                set
                {
                    base.TabStop = value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public new bool Visible
            {
                get
                {
                    return base.Visible;
                }
                set
                {
                    base.Visible = value;
                }
            }

            internal CenteredPanel Owner
            {
                get
                {
                    return this.m_Owner;
                }
            }
            #endregion

            #region Nested Types
            #endregion
        }

        internal class CenteredPanelDesigner : ParentControlDesigner
        {
            #region Variables

            private IDesignerHost designerHost;

            private bool disableDrawGrid;

            private bool containerSelected;

            private CenteredPanel centeredPanel;
            private CenteredPanelPage selectedPanel;
            private CenteredPanelPage interiorPage;
            #endregion

            #region Constructor
            #endregion

            #region Static Methods
            private static CenteredPanelPage CheckIfPanelSelected(object comp)
            {
                return (comp as CenteredPanelPage);
            }
            #endregion

            #region Override Methods
            public override bool CanParent(Control control)
            {
                return false;
            }

            protected override IComponent[] CreateToolCore(System.Drawing.Design.ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
            {
                if (this.Selected == null)
                {
                    this.Selected = this.interiorPage;
                }

                PageDesigner toInvoke = (PageDesigner)this.designerHost.GetDesigner(this.Selected);
                ParentControlDesigner.InvokeCreateTool(toInvoke, tool);
                return null;
            }

            public override void Initialize(IComponent component)
            {
                base.Initialize(component);

                this.centeredPanel = component as CenteredPanel;
                this.interiorPage = this.centeredPanel.InteriorPage;

                base.EnableDesignMode(this.interiorPage, interiorPageName);

                this.designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
                if (this.selectedPanel == null)
                {
                    this.Selected = this.interiorPage;
                }

                this.centeredPanel.MouseDown += new MouseEventHandler(this.OnParentContainer);

                ISelectionService service = (ISelectionService)this.GetService(typeof(ISelectionService));
                if (service != null)
                {
                    service.SelectionChanged += new EventHandler(this.OnSelectionChanged);
                }
            }

            protected override void Dispose(bool disposing)
            {
                ISelectionService service = (ISelectionService)this.GetService(typeof(ISelectionService));
                if (service != null)
                {
                    service.SelectionChanged -= new EventHandler(this.OnSelectionChanged);
                }

                this.centeredPanel.MouseDown -= new MouseEventHandler(this.OnParentContainer);
                base.Dispose(disposing);
            }

            protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
            {
                return base.GetControlGlyph(selectionType);
            }

            protected override Control GetParentForComponent(IComponent component)
            {
                return this.interiorPage;
            }

            protected override bool GetHitTest(Point point)
            {
                return ((this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly) && this.containerSelected);
            }

            public override ControlDesigner InternalControlDesigner(int internalControlIndex)
            {
                CenteredPanelPage panel;
                switch (internalControlIndex)
                {
                    case 0:
                        panel = this.interiorPage;
                        break;

                    default:
                        return null;
                }
                return (this.designerHost.GetDesigner(panel) as ControlDesigner);
            }

            public override int NumberOfInternalControlDesigners()
            {
                return 1;
            }

            protected override void OnDragEnter(DragEventArgs de)
            {
                de.Effect = DragDropEffects.None;
            }

            protected override void OnPaintAdornments(PaintEventArgs pe)
            {
                try
                {
                    this.disableDrawGrid = true;
                    base.OnPaintAdornments(pe);
                }
                finally
                {
                    this.disableDrawGrid = false;
                }
            }
            #endregion

            #region Private Methods
            private void OnSelectionChanged(object sender, EventArgs e)
            {
                ISelectionService service = (ISelectionService)this.GetService(typeof(ISelectionService));
                this.containerSelected = false;

                if (service != null)
                {
                    foreach (object obj2 in service.GetSelectedComponents())
                    {
                        CenteredPanelPage panel = CheckIfPanelSelected(obj2);
                        if ((panel != null) && (panel.Parent == this.centeredPanel))
                        {
                            this.containerSelected = false;
                            this.Selected = panel;
                            break;
                        }

                        this.Selected = null;
                        if (obj2 == this.centeredPanel)
                        {
                            this.containerSelected = true;
                            break;
                        }
                    }
                }
            }

            private void OnParentContainer(object sender, MouseEventArgs e)
            {
                ((ISelectionService)this.GetService(typeof(ISelectionService))).SetSelectedComponents(new object[] { this.Control });
            }
            #endregion

            #region Public Methods
            internal void PageHover()
            {
                this.OnMouseHover();
            }
            #endregion

            #region Properties
            protected override bool AllowControlLasso
            {
                get
                {
                    return false;
                }
            }

            public override ICollection AssociatedComponents
            {
                get
                {
                    ArrayList list = new ArrayList();
                    foreach (CenteredPanelPage panel in this.centeredPanel.Controls)
                    {
                        foreach (Control control in panel.Controls)
                        {
                            list.Add(control);
                        }
                    }
                    return list;
                }
            }

            protected override bool DrawGrid
            {
                get
                {
                    if (this.disableDrawGrid)
                    {
                        return false;
                    }
                    return base.DrawGrid;
                }
            }

            internal CenteredPanelPage Selected
            {
                get
                {
                    return this.selectedPanel;
                }
                set
                {
                    if (this.selectedPanel != null)
                    {
                        PageDesigner designer = (PageDesigner)this.designerHost.GetDesigner(this.selectedPanel);
                        designer.Selected = false;
                    }
                    if (value != null)
                    {
                        PageDesigner designer2 = (PageDesigner)this.designerHost.GetDesigner(value);
                        this.selectedPanel = value;
                        designer2.Selected = true;
                    }
                    else if (this.selectedPanel != null)
                    {
                        PageDesigner designer3 = (PageDesigner)this.designerHost.GetDesigner(this.selectedPanel);
                        this.selectedPanel = null;
                        designer3.Selected = false;
                    }
                }
            }
            #endregion

            #region Nested Types
            #endregion
        }

        internal class PageDesigner : ScrollableControlDesigner
        {
            #region Variables
            private IDesignerHost designerHost;
            private bool selected;
            private CenteredPanelDesigner containerDesigner;
            private CenteredPanelPage panelPage;
            #endregion

            #region Constructor
            public PageDesigner()
            {
                base.AutoResizeHandles = true;
            }
            #endregion

            #region Internal Methods
            internal void DrawSelectedBorder()
            {
                Control control = this.Control;
                Rectangle clientRectangle = control.ClientRectangle;
                using (Graphics graphics = control.CreateGraphics())
                {
                    Color color;
                    if (control.BackColor.GetBrightness() < 0.5)
                    {
                        color = ControlPaint.Light(control.BackColor);
                    }
                    else
                    {
                        color = ControlPaint.Dark(control.BackColor);
                    }
                    using (Pen pen = new Pen(color))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        clientRectangle.Inflate(-4, -4);
                        graphics.DrawRectangle(pen, clientRectangle);
                    }
                }
            }

            internal void DrawWaterMark(Graphics g)
            {
                Control control = this.Control;
                Rectangle clientRectangle = control.ClientRectangle;
                string name = control.Name;
                using (Font font = new Font("Arial", 8f))
                {
                    int x = (clientRectangle.Width / 2) - (((int)g.MeasureString(name, font).Width) / 2);
                    int y = clientRectangle.Height / 2;
                    TextRenderer.DrawText(g, name, font, new Point(x, y), Color.Black, TextFormatFlags.GlyphOverhangPadding);
                }
            }

            internal void EraseBorder()
            {
                Control control = this.Control;
                Rectangle clientRectangle = control.ClientRectangle;
                Graphics graphics = control.CreateGraphics();
                Pen pen = new Pen(control.BackColor);
                pen.DashStyle = DashStyle.Dash;
                clientRectangle.Inflate(-4, -4);
                graphics.DrawRectangle(pen, clientRectangle);
                pen.Dispose();
                graphics.Dispose();
                control.Invalidate();
            }

            internal void DrawBorder(Graphics graphics)
            {
                CenteredPanelPage component = (CenteredPanelPage)base.Component;
                if ((component != null) && component.Visible)
                {
                    Pen borderPen = this.BorderPen;
                    Rectangle clientRectangle = this.Control.ClientRectangle;
                    clientRectangle.Width--;
                    clientRectangle.Height--;
                    graphics.DrawRectangle(borderPen, clientRectangle);
                    borderPen.Dispose();
                }
            }

            #endregion

            #region Override Methods

            public override void Initialize(IComponent component)
            {
                base.Initialize(component);

                this.panelPage = (CenteredPanelPage)component;
                this.designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
                this.containerDesigner = (CenteredPanelDesigner)this.designerHost.GetDesigner(this.panelPage.Parent);

                IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));

                if (service != null)
                {
                    service.ComponentChanged += new ComponentChangedEventHandler(this.OnComponentChanged);
                }

                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(component)["Locked"];
                if ((descriptor != null) && (this.panelPage.Parent is CenteredPanel))
                {
                    descriptor.SetValue(component, true);
                }
            }

            protected override void Dispose(bool disposing)
            {
                IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
                if (service != null)
                {
                    service.ComponentChanged -= new ComponentChangedEventHandler(this.OnComponentChanged);
                }
                base.Dispose(disposing);
            }

            protected override void OnDragDrop(DragEventArgs de)
            {
                if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
                {
                    de.Effect = DragDropEffects.None;
                }
                else
                {
                    base.OnDragDrop(de);
                }
            }

            protected override void OnDragEnter(DragEventArgs de)
            {
                if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
                {
                    de.Effect = DragDropEffects.None;
                }
                else
                {
                    base.OnDragEnter(de);
                }
            }

            protected override void OnDragLeave(EventArgs e)
            {
                if (this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly)
                {
                    base.OnDragLeave(e);
                }
            }

            protected override void OnDragOver(DragEventArgs de)
            {
                if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
                {
                    de.Effect = DragDropEffects.None;
                }
                else
                {
                    base.OnDragOver(de);
                }
            }

            protected override void OnMouseHover()
            {
                if (this.containerDesigner != null)
                {
                    this.containerDesigner.PageHover();
                }
            }

            protected override void OnPaintAdornments(PaintEventArgs pe)
            {
                CenteredPanelPage component = (CenteredPanelPage)base.Component;

                if (component.BorderStyle == BorderStyle.None)
                {
                    this.DrawBorder(pe.Graphics);
                }

                base.OnPaintAdornments(pe);

                if (this.panelPage.BorderStyle == BorderStyle.None)
                {
                    this.DrawBorder(pe.Graphics);
                }

                if (this.Selected)
                {
                    this.DrawSelectedBorder();
                }

                if (this.panelPage.Controls.Count == 0)
                {
                    this.DrawWaterMark(pe.Graphics);
                }
            }

            protected override void PreFilterProperties(IDictionary properties)
            {
                base.PreFilterProperties(properties);
                properties.Remove("Modifiers");
                properties.Remove("Locked");
                properties.Remove("GenerateMember");

                foreach (DictionaryEntry entry in properties)
                {
                    PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)entry.Value;
                    if (oldPropertyDescriptor.Name.Equals("Name") && oldPropertyDescriptor.DesignTimeOnly)
                    {
                        properties[entry.Key] = TypeDescriptor.CreateProperty(oldPropertyDescriptor.ComponentType, oldPropertyDescriptor, new Attribute[] { BrowsableAttribute.No, DesignerSerializationVisibilityAttribute.Hidden });
                        break;
                    }
                }
            }

            public override bool CanBeParentedTo(IDesigner parentDesigner)
            {
                return (parentDesigner is CenteredPanelDesigner);
            }
            #endregion

            #region Private Methods
            private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
            {
                if (this.panelPage.Parent != null)
                {
                    if (this.panelPage.Controls.Count == 0)
                    {
                        Graphics g = this.panelPage.CreateGraphics();
                        this.DrawWaterMark(g);
                        g.Dispose();
                    }
                    else
                    {
                        this.panelPage.Invalidate();
                    }
                }
            }
            #endregion

            #region Properties
            protected override InheritanceAttribute InheritanceAttribute
            {
                get
                {
                    if ((this.panelPage != null) && (this.panelPage.Parent != null))
                    {
                        return (InheritanceAttribute)TypeDescriptor.GetAttributes(this.panelPage.Parent)[typeof(InheritanceAttribute)];
                    }
                    return base.InheritanceAttribute;
                }
            }

            internal bool Selected
            {
                get
                {
                    return this.selected;
                }
                set
                {
                    this.selected = value;
                    if (this.selected)
                    {
                        this.DrawSelectedBorder();
                    }
                    else
                    {
                        this.EraseBorder();
                    }
                }
            }

            protected Pen BorderPen
            {
                get
                {
                    Color color = (this.Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(this.Control.BackColor) : ControlPaint.Dark(this.Control.BackColor);
                    Pen pen = new Pen(color);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    return pen;
                }
            }

            public override SelectionRules SelectionRules
            {
                get
                {
                    SelectionRules selectionRules = base.SelectionRules;
                    if ((this.Control.Parent is CenteredPanel) && (this.Component is CenteredPanelPage))
                    {
                        selectionRules &= ~SelectionRules.Moveable;
                    }
                    return selectionRules;
                }
            }

            public override IList SnapLines
            {
                get
                {
                    ArrayList snapLines = null;
                    base.AddPaddingSnapLines(ref snapLines);
                    return snapLines;
                }
            }
            #endregion
        }
        #endregion
    }
}
