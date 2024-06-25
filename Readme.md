<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128648649/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4025)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [ViewModel.cs](./CS/DXGrid_ConditionalFormatting/ViewModel.cs) (VB: [ViewModel.vb](./VB/DXGrid_ConditionalFormatting/ViewModel.vb))
* [Window1.xaml](./CS/DXGrid_ConditionalFormatting/Window1.xaml) (VB: [Window1.xaml](./VB/DXGrid_ConditionalFormatting/Window1.xaml))
* [Window1.xaml.cs](./CS/DXGrid_ConditionalFormatting/Window1.xaml.cs) (VB: [Window1.xaml](./VB/DXGrid_ConditionalFormatting/Window1.xaml))
<!-- default file list end -->
# How to change a modified cell appearance 


<p>Our controls provide the built-in way to show the current <a href="https://documentation.devexpress.com/WPF/17130/Controls-and-Libraries/Data-Grid/Conditional-Formatting">Conditional Formatting</a> with an animated effect. All you need is to apply the required conditional formatting and then set the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.AnimateConditionalFormattingTransition.property">TableView.AnimateConditionalFormattingTransition</a> property to True.<br><br><em>For versions prior to 17.1:</em><br>On the object level implement the Dictionary collection that includes a property-state pair to determine the cell state for the current object property.Change the ValueState value when a cell is being modified and call the PropertyChanged method to inform the GridControl that it is necessary to refresh the cell appearance. In the sample, the INotifyPropertyChanged interface is supported. Our GridControl receives notification from this interface. This method is present in the INotifyPropertyChanged interface that should be supported on the object level.<br></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-use-format-conditions-to-animate-data-changes&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-use-format-conditions-to-animate-data-changes&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
