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
				Products.Add(New Product() With {
					.Name = "Product" & i,
					.OldPrice = oldPrice,
					.NewPrice=newPrice,
					.DeltaPrice = newPrice - oldPrice,
					.IsAvailable = (i Mod 2 = 0)
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

'INSTANT VB NOTE: The field name was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private name_Conflict As String
'INSTANT VB NOTE: The field oldPrice was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private oldPrice_Conflict As Decimal
'INSTANT VB NOTE: The field newPrice was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private newPrice_Conflict As Decimal
'INSTANT VB NOTE: The field deltaPrice was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private deltaPrice_Conflict As Decimal
'INSTANT VB NOTE: The field isAvailable was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private isAvailable_Conflict As Boolean

		Public Property Name() As String
			Get
				Return name_Conflict
			End Get
			Set(ByVal value As String)
				name_Conflict = value
				RaisePropertyChanged("Name")
			End Set
		End Property
		Public Property OldPrice() As Decimal
			Get
				Return oldPrice_Conflict
			End Get
			Set(ByVal value As Decimal)
				oldPrice_Conflict = value
				RaisePropertyChanged("OldPrice")
			End Set
		End Property
		Public Property NewPrice() As Decimal
			Get
				Return newPrice_Conflict
			End Get
			Set(ByVal value As Decimal)
				newPrice_Conflict = value
				RaisePropertyChanged("NewPrice")
			End Set
		End Property
		Public Property DeltaPrice() As Decimal
			Get
				Return deltaPrice_Conflict
			End Get
			Set(ByVal value As Decimal)
				deltaPrice_Conflict = value
				RaisePropertyChanged("DeltaPrice")
			End Set
		End Property
		Public Property IsAvailable() As Boolean
			Get
				Return isAvailable_Conflict
			End Get
			Set(ByVal value As Boolean)
				isAvailable_Conflict = value
				RaisePropertyChanged("IsAvailable")
			End Set
		End Property

		Public Event PropertyChanged As PropertyChangedEventHandler

		Protected Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
