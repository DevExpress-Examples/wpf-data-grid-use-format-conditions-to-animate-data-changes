Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Windows.Threading

Namespace DXGrid_ConditionalFormatting

    Public Class ViewModel

        Public Property Products As ObservableCollection(Of DXGrid_ConditionalFormatting.Product)

        Private updateTimer As System.Windows.Threading.DispatcherTimer = New System.Windows.Threading.DispatcherTimer()

        Public Sub New()
            Me.PopulateData()
            Me.SetUpdateTimer()
        End Sub

        Protected Sub PopulateData()
            Me.Products = New System.Collections.ObjectModel.ObservableCollection(Of DXGrid_ConditionalFormatting.Product)()
            Dim randomPriceValue As System.Random = New System.Random()
            For i As Integer = 0 To 30 - 1
                Dim oldPrice As Decimal =(CDec(randomPriceValue.[Next](1000)) / 10)
                Dim newPrice As Decimal =(CDec(randomPriceValue.[Next](10000)) / 10)
                Me.Products.Add(New DXGrid_ConditionalFormatting.Product() With {.Name = "Product" & i, .OldPrice = oldPrice, .NewPrice = newPrice, .DeltaPrice = newPrice - oldPrice, .IsAvailable =(i Mod 2 = 0)})
            Next
        End Sub

        Private Sub SetUpdateTimer()
            AddHandler Me.updateTimer.Tick, AddressOf Me.updateTimer_Tick
            Me.updateTimer.Interval = New System.TimeSpan(0, 0, 2)
            Me.updateTimer.Start()
        End Sub

        Private Sub updateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim rnd As System.Random = New System.Random()
            Dim updateProductNumber As Integer
            For i As Integer = 0 To 3 - 1
                updateProductNumber = rnd.[Next](Me.Products.Count)
                Me.Products(CInt((updateProductNumber))).IsAvailable = Not Me.Products(CInt((updateProductNumber))).IsAvailable
            Next

            For i As Integer = 0 To 5 - 1
                updateProductNumber = rnd.[Next](Me.Products.Count)
                Dim oldPrice As Decimal =(CDec(rnd.[Next](1000)) / 10)
                Dim newPrice As Decimal =(CDec(rnd.[Next](10000)) / 10)
                Me.Products(CInt((updateProductNumber))).DeltaPrice = newPrice - oldPrice
                Me.Products(CInt((updateProductNumber))).OldPrice = oldPrice
                Me.Products(CInt((updateProductNumber))).NewPrice = newPrice
            Next
        End Sub
    End Class

    Public Class Product
        Implements System.ComponentModel.INotifyPropertyChanged

        Private nameField As String

        Private oldPriceField As Decimal

        Private newPriceField As Decimal

        Private deltaPriceField As Decimal

        Private isAvailableField As Boolean

        Public Property Name As String
            Get
                Return Me.nameField
            End Get

            Set(ByVal value As String)
                Me.nameField = value
                Me.RaisePropertyChanged("Name")
            End Set
        End Property

        Public Property OldPrice As Decimal
            Get
                Return Me.oldPriceField
            End Get

            Set(ByVal value As Decimal)
                Me.oldPriceField = value
                Me.RaisePropertyChanged("OldPrice")
            End Set
        End Property

        Public Property NewPrice As Decimal
            Get
                Return Me.newPriceField
            End Get

            Set(ByVal value As Decimal)
                Me.newPriceField = value
                Me.RaisePropertyChanged("NewPrice")
            End Set
        End Property

        Public Property DeltaPrice As Decimal
            Get
                Return Me.deltaPriceField
            End Get

            Set(ByVal value As Decimal)
                Me.deltaPriceField = value
                Me.RaisePropertyChanged("DeltaPrice")
            End Set
        End Property

        Public Property IsAvailable As Boolean
            Get
                Return Me.isAvailableField
            End Get

            Set(ByVal value As Boolean)
                Me.isAvailableField = value
                Me.RaisePropertyChanged("IsAvailable")
            End Set
        End Property

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements Global.System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
