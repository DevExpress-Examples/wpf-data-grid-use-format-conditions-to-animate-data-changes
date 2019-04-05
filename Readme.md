<!-- default file list -->
*Files to look at*:

* [Window1.xaml](./CS/DXGrid_ConditionalFormatting/Window1.xaml) (VB: [Window1.xaml](./VB/DXGrid_ConditionalFormatting/Window1.xaml))
* [Window1.xaml.cs](./CS/DXGrid_ConditionalFormatting/Window1.xaml.cs) (VB: [Window1.xaml.vb](./VB/DXGrid_ConditionalFormatting/Window1.xaml.vb))
<!-- default file list end -->
# How to change a modified cell appearance 


<p>Our controls provide the built-in way to show the current <a href="https://documentation.devexpress.com/WPF/17130/Controls-and-Libraries/Data-Grid/Conditional-Formatting">Conditional Formatting</a> with an animated effect. All you need is to apply the required conditional formatting and then set the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.AnimateConditionalFormattingTransition.property">TableView.AnimateConditionalFormattingTransition</a> property to True.<br><br><em>For versions prior to 17.1:</em><br>On the object level implement the Dictionary collection that includes a property-state pair to determine the cell state for the current object property.Change the ValueState value when a cell is being modified and call the PropertyChanged method to inform the GridControl that it is necessary to refresh the cell appearance. In the sample, the INotifyPropertyChanged interface is supported. Our GridControl receives notification from this interface. This method is present in the INotifyPropertyChanged interface that should be supported on the object level.<br></p>

<br/>


