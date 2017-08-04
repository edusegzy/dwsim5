﻿Imports DWSIM.Drawing.SkiaSharp.GraphicObjects
Imports DWSIM.Interfaces.Enums.GraphicObjects

Namespace GraphicObjects.Shapes

    Public Class SpecGraphic

        Inherits ShapeGraphic

      Protected m_svPT, m_tvPT, m_rvPT As GraphicObject

        Public Property ConnectedToSv() As GraphicObject
            Get
                Return m_svPT
            End Get
            Set(ByVal value As GraphicObject)
                m_svPT = value
            End Set
        End Property

        Public Property ConnectedToTv() As GraphicObject
            Get
                Return m_tvPT
            End Get
            Set(ByVal value As GraphicObject)
                m_tvPT = value
            End Set
        End Property

#Region "Constructors"

        Public Sub New()
            Me.ObjectType = DWSIM.Interfaces.Enums.GraphicObjects.ObjectType.OT_Spec
            Me.Description = "Spec Logical Op"
        End Sub

        Public Sub New(ByVal graphicPosition As SKPoint)
            Me.New()
            Me.SetPosition(graphicPosition)
        End Sub

        Public Sub New(ByVal posX As Integer, ByVal posY As Integer)
            Me.New(New SKPoint(posX, posY))
        End Sub

        Public Sub New(ByVal graphicPosition As SKPoint, ByVal graphicSize As SKSize)
            Me.New(graphicPosition)
            Me.SetSize(graphicSize)
        End Sub

        Public Sub New(ByVal posX As Integer, ByVal posY As Integer, ByVal graphicSize As SKSize)
            Me.New(New SKPoint(posX, posY), graphicSize)
        End Sub

        Public Sub New(ByVal posX As Integer, ByVal posY As Integer, ByVal width As Integer, ByVal height As Integer)
            Me.New(New SKPoint(posX, posY), New SKSize(width, height))
        End Sub

#End Region

        Public Overrides Sub CreateConnectors(InCount As Integer, OutCount As Integer)

            Me.EnergyConnector.Active = False

        End Sub

        Public Overrides Sub Draw(ByVal g As Object)

            Dim canvas As SKCanvas = DirectCast(g, SKCanvas)

            CreateConnectors(0, 0)

            UpdateStatus()

            MyBase.Draw(g)

            Dim aPen As New SKPaint()
            With aPen
                .Color = SKColors.Blue
                .StrokeWidth = LineWidth
                .IsStroke = True
                .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
                .PathEffect = SKPathEffect.CreateDash(New Single() {10.0F, 5.0F, 2.0F, 5.0F}, 2.0F)
            End With

            If Not Me.ConnectedToSv Is Nothing Then
                canvas.DrawPoints(SKPointMode.Polygon, New SKPoint() {New SKPoint(Me.X + Me.Width / 2, Me.Y + Me.Height / 2), New SKPoint(Me.m_svPT.X, Me.Y + Me.Height / 2), Me.m_svPT.GetPosition}, aPen)
            End If
            If Not Me.ConnectedToTv Is Nothing Then
                canvas.DrawPoints(SKPointMode.Polygon, New SKPoint() {New SKPoint(Me.X + Me.Width / 2, Me.Y + Me.Height / 2), New SKPoint(Me.m_tvPT.X, Me.Y + Me.Height / 2), Me.m_tvPT.GetPosition}, aPen)
            End If

            Dim myPen As New SKPaint()
            With myPen
                .Color = SKColors.LightBlue
                .StrokeWidth = LineWidth
                .IsStroke = False
                .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
            End With

            canvas.DrawOval(New SKRect(X, Y, X + Width, Y + Height), myPen)

            Dim myPen2 As New SKPaint()
            With myPen2
                .Color = SKColors.Blue
                .StrokeWidth = LineWidth
                .IsStroke = True
                .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
            End With

            canvas.DrawOval(New SKRect(X, Y, X + Width, Y + Height), myPen2)

            Dim tpaint As New SKPaint()

            With tpaint
                .TextSize = 18.0#
                .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
                .Color = SKColors.Blue
                .IsStroke = False
                .Typeface = DefaultTypeFace
            End With

            Dim trect As New SKRect(0, 0, 2, 2)
            tpaint.GetTextPath("S", 0, 0).GetBounds(trect)

            Dim ax, ay As Integer
            ax = Me.X + (Me.Width - (trect.Right - trect.Left)) / 2
            ay = Me.Y + (Me.Height - (trect.Top - trect.Bottom)) / 2

            canvas.DrawText("S", ax, ay, tpaint)


        End Sub

    End Class

End Namespace