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
        Public Property Products() As ObservableCollection(Of Product)
        Private updateTimer As New DispatcherTimer()
        Public Sub New()
            PopulateData()
            SetUpdateTimer()
        End Sub
        Protected Sub PopulateData()
            Products = New ObservableCollection(Of Product)()
            Dim randomPriceValue As New Random()
            For i As Integer = 0 To 29
                Dim oldPrice As Decimal = (CDec(randomPriceValue.Next(1000)) / 10)
                Dim newPrice As Decimal = (CDec(randomPriceValue.Next(10000)) / 10)
                Products.Add(New Product() With { _
                    .Name = "Product" & i, _
                    .OldPrice = oldPrice, _
                    .NewPrice=newPrice, _
                    .DeltaPrice = newPrice - oldPrice, _
                    .IsAvailable = (i Mod 2 = 0) _
                })
            Next i
        End Sub
        Private Sub SetUpdateTimer()
            AddHandler updateTimer.Tick, AddressOf updateTimer_Tick
            updateTimer.Interval = New TimeSpan(0, 0, 2)
            updateTimer.Start()
        End Sub
        Private Sub updateTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            Dim rnd As New Random()
            Dim updateProductNumber As Integer
            For i As Integer = 0 To 2
                updateProductNumber = rnd.Next(Products.Count)
                Products(updateProductNumber).IsAvailable = Not Products(updateProductNumber).IsAvailable
            Next i
            For i As Integer = 0 To 4
                updateProductNumber = rnd.Next(Products.Count)

                Dim oldPrice As Decimal = (CDec(rnd.Next(1000)) / 10)
                Dim newPrice As Decimal = (CDec(rnd.Next(10000)) / 10)

                Products(updateProductNumber).DeltaPrice = newPrice - oldPrice

                Products(updateProductNumber).OldPrice = oldPrice
                Products(updateProductNumber).NewPrice = newPrice
            Next i
        End Sub
    End Class

    Public Class Product
        Implements INotifyPropertyChanged


        Private name_Renamed As String

        Private oldPrice_Renamed As Decimal

        Private newPrice_Renamed As Decimal

        Private deltaPrice_Renamed As Decimal

        Private isAvailable_Renamed As Boolean

        Public Property Name() As String
            Get
                Return name_Renamed
            End Get
            Set(ByVal value As String)
                name_Renamed = value
                RaisePropertyChanged("Name")
            End Set
        End Property
        Public Property OldPrice() As Decimal
            Get
                Return oldPrice_Renamed
            End Get
            Set(ByVal value As Decimal)
                oldPrice_Renamed = value
                RaisePropertyChanged("OldPrice")
            End Set
        End Property
        Public Property NewPrice() As Decimal
            Get
                Return newPrice_Renamed
            End Get
            Set(ByVal value As Decimal)
                newPrice_Renamed = value
                RaisePropertyChanged("NewPrice")
            End Set
        End Property
        Public Property DeltaPrice() As Decimal
            Get
                Return deltaPrice_Renamed
            End Get
            Set(ByVal value As Decimal)
                deltaPrice_Renamed = value
                RaisePropertyChanged("DeltaPrice")
            End Set
        End Property
        Public Property IsAvailable() As Boolean
            Get
                Return isAvailable_Renamed
            End Get
            Set(ByVal value As Boolean)
                isAvailable_Renamed = value
                RaisePropertyChanged("IsAvailable")
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
