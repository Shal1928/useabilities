using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Common.Controls.InfoGraphic.Enums;

namespace Common.Controls.InfoGraphic.InfoIoCalendar
{
    /// <summary>
    /// Interaction logic for InfoIoCalendar.xaml
    /// </summary>
    public partial class InfoIoCalendar : UserControl
    {
        #region Provate Properties

        private List<DataGridColumn> _weekColumnsCollection = new List<DataGridColumn>
                                                                 {
                                                                    new DataGridTextColumn{Header = "Пн"},
                                                                    new DataGridTextColumn{Header = "Вт"},
                                                                    new DataGridTextColumn{Header = "Ср"},
                                                                    new DataGridTextColumn{Header = "Чт"},
                                                                    new DataGridTextColumn{Header = "Пт"},
                                                                    new DataGridTextColumn{Header = "Сб"},
                                                                    new DataGridTextColumn{Header = "Вс"}
                                                                 };

        private List<DataGridColumn> _monthColumnsCollection = new List<DataGridColumn>
                                                                 {
                                                                    new DataGridTextColumn{Header = "First period"},
                                                                    new DataGridTextColumn{Header = "Second period"},
                                                                    new DataGridTextColumn{Header = "Third period"},
                                                                    new DataGridTextColumn{Header = "Fourth period"},
                                                                    new DataGridTextColumn{Header = "Fifth period"}
                                                                 };

        private List<DataGridColumn> _yearColumnsCollection = new List<DataGridColumn>
                                                                 {
                                                                    new DataGridTextColumn{Header = "Январь"},
                                                                    new DataGridTextColumn{Header = "Февраль"},
                                                                    new DataGridTextColumn{Header = "Март"},
                                                                    new DataGridTextColumn{Header = "Апрель"},
                                                                    new DataGridTextColumn{Header = "Май"},
                                                                    new DataGridTextColumn{Header = "Июнь"},
                                                                    new DataGridTextColumn{Header = "Июль"},
                                                                    new DataGridTextColumn{Header = "Август"},
                                                                    new DataGridTextColumn{Header = "Сентябрь"},
                                                                    new DataGridTextColumn{Header = "Октябрь"},
                                                                    new DataGridTextColumn{Header = "Ноябрь"},
                                                                    new DataGridTextColumn{Header = "Декабрь"},
                                                                 };

        #endregion


        #region Constructors



        #endregion

        public InfoIoCalendar()
        {
            InitializeComponent();
        }

        static InfoIoCalendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoIoCalendar), new FrameworkPropertyMetadata(typeof(InfoIoCalendar)));
        }

        #region TemporalPeriodProperty

        public static readonly DependencyProperty TemporalPeriodProperty =
            DependencyProperty.Register("TemporalPeriod",
                                        typeof(TemporalPeriodMode),
                                        typeof(InfoIoCalendar),
                                        new UIPropertyMetadata(null, TemporalPeriodPropertyChanged)
                                       );

        public TemporalPeriodMode TemporalPeriod
        {
            get
            {
                return (TemporalPeriodMode)GetValue(TemporalPeriodProperty);
            }
            set
            {
                SetValue(TemporalPeriodProperty, value);
            }
        }

        private static void TemporalPeriodPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var infoIoCalendar = d as InfoIoCalendar;
            var temporalPeriodMode = (TemporalPeriodMode)e.NewValue;

            infoIoCalendar.dataGrid.Columns.Clear();
            
            //var columns = e.NewValue as ObservableCollection<DataGridColumn>;

            //dataGrid.Columns.Clear();
            //if (columns == null) return;

            //foreach (var column in columns)
            //    dataGrid.Columns.Add(column);

            switch (temporalPeriodMode)
            {
                case TemporalPeriodMode.Day:
                    throw new NotImplementedException();

                case TemporalPeriodMode.Week:
                    
                    break;

                case TemporalPeriodMode.Month:
                    break;

                case TemporalPeriodMode.Year:
                    break;

                case TemporalPeriodMode.Age:
                    throw new NotImplementedException();

                default:
                    break;
            }


            //infoIoCalendar.dataGrid.Columns.
            //var binding = new Binding
            //                  {
            //                      Path = new PropertyPath("DataGridPropertyExtension.BindableColumns")
            //                  };

            //if (infoIoCalendar != null) 
            //    infoIoCalendar.dataGrid.SetBinding(DataGridPropertyExtension.BindableColumnsProperty, binding);
        }

        /*
         
        <Canvas>
            <TextBox x:Name="textBox" Text="{Binding ElementName=button, Path=(Canvas.Left)}" />
            <Button x:Name="button" Content="Press me" />
        </Canvas>
        
        Binding binding = new Binding();
        binding.Source = button;
        binding.Path = new PropertyPath("Canvas.Left");
        textBox.SetBinding(TextBlock.TextProperty, binding);
        
         
         */

        #endregion


    }
}
