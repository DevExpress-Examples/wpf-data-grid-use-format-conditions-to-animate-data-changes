Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.ComponentModel
Imports System
Imports DevExpress.Xpf.Grid
Imports System.Windows.Controls

Namespace DXGrid_ConditionalFormatting
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = Products.GetData()

		End Sub
	End Class

	Public Class ColumnRowIndexesCellValueConverter
		Inherits MarkupExtension
		Implements IMultiValueConverter
		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function

		#Region "IMultiValueConverter Members"

		Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
			If TypeOf values(0) Is GridColumn AndAlso values(1) IsNot Nothing Then
				Dim column As GridColumn = CType(values(0), GridColumn)
				Dim cellValue As Object = TypeDescriptor.GetProperties(values(1))(column.FieldName).GetValue(values(1))
				Dim dict As Dictionary(Of String, ValueState) = CType(TypeDescriptor.GetProperties(values(1))("States").GetValue(values(1)), Dictionary(Of String, ValueState))
				Dim state As ValueState
				dict.TryGetValue(column.FieldName, state)
				If state = ValueState.Changed Then
					Return Brushes.Red
				Else
					Return Brushes.Transparent
				End If
			End If
			Return Nothing
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function

		#End Region
	End Class
	Public Class Products
		Public Shared Function GetData() As List(Of Product)
			Dim data As New List(Of Product)()
			data.Add(New Product() With {.ProductName = "Ipoh Coffee", .UnitPrice = 12, .UnitsOnOrder = 2})
			data.Add(New Product() With {.ProductName = "Outback Lager", .UnitPrice = 25, .UnitsOnOrder = 2})
			data.Add(New Product() With {.ProductName = "Boston Crab Meat", .UnitPrice = 18, .UnitsOnOrder = 3332})
			data.Add(New Product() With {.ProductName = "Konbu", .UnitPrice = 24, .UnitsOnOrder = 111})
			Return data
		End Function
	End Class
	Public Enum ValueState
		Original
		Changed
	End Enum
	Public Class Product
		Implements INotifyPropertyChanged
		Private productName_Renamed As String
		Public Property ProductName() As String
			Get
				Return productName_Renamed
			End Get
			Set(ByVal value As String)
				If productName_Renamed = value Then
					Return
				End If
				If productName_Renamed Is Nothing Then
					productName_Renamed = value
					Return
				End If
				productName_Renamed = value
				RaisePropertyChanged("ProductName")
				States("ProductName") = ValueState.Changed
				RaisePropertyChanged("States")
			End Set
		End Property

		Private unitPrice_Renamed As Integer
		Private unitPriceIsAssignedFirstTime As Boolean = True
		Public Property UnitPrice() As Integer
			Get
				Return unitPrice_Renamed
			End Get
			Set(ByVal value As Integer)
				If unitPrice_Renamed = value Then
					Return
				End If
				unitPrice_Renamed = value
				If (Not unitPriceIsAssignedFirstTime) Then
					RaisePropertyChanged("UnitPrice")
					States("UnitPrice") = ValueState.Changed
					RaisePropertyChanged("States")
				Else
					unitPriceIsAssignedFirstTime = False
				End If
			End Set
		End Property

		Private unitsOnOrder_Renamed As Integer
		Private unitsOnOrderIsAssignedFirstTime As Boolean = True
		Public Property UnitsOnOrder() As Integer
			Get
				Return unitsOnOrder_Renamed
			End Get
			Set(ByVal value As Integer)
				If unitsOnOrder_Renamed = value Then
					Return
				End If
				unitsOnOrder_Renamed = value
				If (Not unitsOnOrderIsAssignedFirstTime) Then
					RaisePropertyChanged("UnitsOnOrder")
					States("UnitsOnOrder") = ValueState.Changed
					RaisePropertyChanged("States")
				Else
					unitsOnOrderIsAssignedFirstTime = False
				End If
			End Set
		End Property


		Private privateStates As Dictionary(Of String, ValueState)
		Public Property States() As Dictionary(Of String, ValueState)
			Get
				Return privateStates
			End Get
			Private Set(ByVal value As Dictionary(Of String, ValueState))
				privateStates = value
			End Set
		End Property
		Public Sub New()
			States = New Dictionary(Of String, ValueState)()
		End Sub

		#Region "INotifyPropertyChanged Members"

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		#End Region

		Private Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
