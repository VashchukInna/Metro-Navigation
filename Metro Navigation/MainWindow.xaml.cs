using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Metro_Navigation
{
    public partial class MainWindow : Window
    {
        private readonly Ellipse[] _allStations;
        private readonly Ellipse[] _redLine;
        private readonly Ellipse[] _blueLine;
        private readonly Ellipse[] _greenLine;
        private readonly Ellipse[] _getAllRed;
        private readonly Ellipse[] _getAllBlue;
        private readonly Ellipse[] _getAllGreen;
        private List<Ellipse> _route;
        private int _count;
        readonly DispatcherTimer _timerColor = new DispatcherTimer();
        readonly DispatcherTimer _timer = new DispatcherTimer();
        private Ellipse _start;
        private Ellipse _stop;
        bool _color = true;

        public MainWindow()
        {
            InitializeComponent();
            _allStations = new[] { Academmistechko,Zhytomyrska,Sviatoshyn,Nyvky,Beresteiska,Shuliavska,Politekhnichnyi_Instytut,Voksalna,
                                   Universytet,Teatralna,Khreshchatyk,Arsenalna,Dnipro,Hidropark,Livoberezhna,Darnytsia,Chernihivska,Lisova,
                                   Heroiv_Dnipra,Minska,Obolon,Petrivka,Tarasa_Shevchenka,Kontraktova_Ploshcha, Poshtova_Ploshcha,Maidan_Nezalezhnosti,
                                   Ploshcha_Lva_Tolstoho,Olimpiiska,Palats_Ukrayina,Lybidska,Demiivska,Holosiivska,Vasylkivska,Vystavkovyi_Tsentr,
                                   Ipodrom,Teremky,Syrets,Dorohozhychi,Lykianivska,Zoloti_Vorota,Palats_Sportu,Klovska,Pecherska,Druzhby_Narodiv,
                                   Vydubychi,Slavutych,Osokorky,Pozniaky,Kharkivska,Boryspilska,Vyrlytsia,Chervonyi_Khutir};
            _redLine = new[] {Academmistechko,Zhytomyrska,Sviatoshyn,Nyvky,Beresteiska,Shuliavska,Politekhnichnyi_Instytut,Voksalna,
                                     Universytet,Teatralna,Khreshchatyk,Arsenalna,Dnipro,Hidropark,Livoberezhna,Darnytsia,Chernihivska,Lisova};
            _blueLine = new[] {Heroiv_Dnipra,Minska,Obolon,Petrivka,Tarasa_Shevchenka,Kontraktova_Ploshcha,
                                      Poshtova_Ploshcha,Maidan_Nezalezhnosti,Ploshcha_Lva_Tolstoho,Olimpiiska,Palats_Ukrayina,Lybidska,Demiivska,
                                      Holosiivska,Vasylkivska,Vystavkovyi_Tsentr,Ipodrom,Teremky};
            _greenLine = new[] {Syrets,Dorohozhychi,Lykianivska,Zoloti_Vorota,Palats_Sportu,Klovska,Pecherska,Druzhby_Narodiv,Vydubychi,
                                       Slavutych,Osokorky,Pozniaky,Kharkivska,Boryspilska,Vyrlytsia,Chervonyi_Khutir};
            _getAllRed = new[] { Academmistechko,R1,R2,Zhytomyrska,R3,Sviatoshyn,R4,Nyvky,R5,Beresteiska,R6,Shuliavska,R7,R8,Politekhnichnyi_Instytut,
                                R9,R10,R11,Voksalna,R12,R13,Universytet,R14,R15,Teatralna,R16,Khreshchatyk,R17,R18,R19, Arsenalna,R20,R21,Dnipro,
                                R22,R23,R24,Hidropark,R25,R26,R27,Livoberezhna,R28,R29,Darnytsia,R30,Chernihivska,R31,Lisova};
            _getAllBlue = new[] { Heroiv_Dnipra, B1,Minska,B2,Obolon,B3,B4,Petrivka,B5,B6,B7,Tarasa_Shevchenka,B8,B9,Kontraktova_Ploshcha,B10,B11,B12,
                                  Poshtova_Ploshcha,B13,B14,B15,Maidan_Nezalezhnosti,B16,B17,Ploshcha_Lva_Tolstoho,B18,Olimpiiska,B19,B20,B21,Palats_Ukrayina,
                                  B22,B23,Lybidska,B24,B25,B26,Demiivska,B27,B28,Holosiivska,B29,B30,B31,B32,Vasylkivska,B33,Vystavkovyi_Tsentr,
                                  B34,Ipodrom,B35,B36,B37,B38,Teremky};
            _getAllGreen = new[] {Syrets,G1,G2,Dorohozhychi,G3,G4,G5,G6,G7,G8,Lykianivska,G9,G10,G11,G12,G13,G14,G15,G16,G17,Zoloti_Vorota,G18,G19,
                                  Palats_Sportu,G20,G21,Klovska,G22,G23,G24,Pecherska,G25,G26,G27,Druzhby_Narodiv,G28,G29,G30,G31,G32,Vydubychi,
                                  G33,G34,G35,G36,G37,G38,G39,Slavutych,G40,Osokorky,G41,Pozniaky,G42,Kharkivska,G43,Boryspilska,G44,Vyrlytsia,G45,Chervonyi_Khutir};
            ClickOnStation();
            _timer.Tick += Timer;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            _timerColor.Tick += ChangeColor;
            _timerColor.Interval = new TimeSpan(0, 0, 0, 0, 200);
        }
 
        private List<Ellipse> Run(Ellipse start, Ellipse stop)
        {
            var request = new List<Ellipse>();
            if (Equals(_start, Ploshcha_Lva_Tolstoho) && Equals(_stop, Teatralna))
            {
                request.Add(Palats_Sportu);
                request.Add(G19);
                request.Add(G18);
                request.Add(Zoloti_Vorota);
                return request;
            }
            if (Equals(_stop, Ploshcha_Lva_Tolstoho) && Equals(_start, Teatralna))
            {
                request.Add(Zoloti_Vorota);
                request.Add(G18);
                request.Add(G19);
                request.Add(Palats_Sportu);
                return request;
            }
            if (Equals(_start, Zoloti_Vorota) && Equals(_stop, Maidan_Nezalezhnosti))
            {
                request.Add(Teatralna);
                request.Add(R16);
                request.Add(Khreshchatyk);
                return request;
            }
            if (Equals(_stop, Zoloti_Vorota) && Equals(_start, Maidan_Nezalezhnosti))
            {
                request.Add(Khreshchatyk);
                request.Add(R16);
                request.Add(Teatralna);
                return request;
            }
            if (Equals(_start, Palats_Sportu) && Equals(_stop, Khreshchatyk))
            {
                request.Add(Ploshcha_Lva_Tolstoho);
                request.Add(B17);
                request.Add(B16);
                request.Add(Maidan_Nezalezhnosti);
                return request;
            }
            if (Equals(_stop, Palats_Sportu) && Equals(_start, Khreshchatyk))
            {
                request.Add(Maidan_Nezalezhnosti);
                request.Add(B16);
                request.Add(B17);
                request.Add(Ploshcha_Lva_Tolstoho);
                return request;
            }
            if (_redLine.Contains(start) && _redLine.Contains(stop))
                request = PathLine(_getAllRed);
            if (_blueLine.Contains(start) && _blueLine.Contains(stop))
                request = PathLine(_getAllBlue);
            if (_greenLine.Contains(start) && _greenLine.Contains(stop))
                request = PathLine(_getAllGreen);
            if (_redLine.Contains(start) && _blueLine.Contains(stop))
                request = PathTwoLine(_getAllRed, _getAllBlue, 27, 23);
            if (_redLine.Contains(start) && _greenLine.Contains(stop))
                request = PathTwoLine(_getAllRed, _getAllGreen, 25, 21);
            if (_blueLine.Contains(start) && _greenLine.Contains(stop))
                request = PathTwoLine(_getAllBlue, _getAllGreen, 26, 24);
            if (_blueLine.Contains(start) && _redLine.Contains(stop))
                request = PathTwoLine(_getAllBlue, _getAllRed, 23, 27);
            if (_greenLine.Contains(start) && _redLine.Contains(stop))
                request = PathTwoLine(_getAllGreen, _getAllRed, 21, 25);
            if (_greenLine.Contains(start) && _blueLine.Contains(stop))
                request = PathTwoLine(_getAllGreen, _getAllBlue, 24, 26);
            return request;
        }

       private List<Ellipse> PathLine(Ellipse[] oneLine)
        {
            var request = new List<Ellipse>();
            Ellipse[] allElements;
            if (int.Parse(_start.Tag.ToString()) < int.Parse(_stop.Tag.ToString()))
            {
                allElements = oneLine.OrderBy(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in allElements)
                    if (int.Parse(i.Tag.ToString()) > int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < int.Parse(_stop.Tag.ToString()))
                        request.Add(i);
            }
            if (int.Parse(_start.Tag.ToString()) > int.Parse(_stop.Tag.ToString()))
            {
                allElements = oneLine.OrderByDescending(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in allElements)
                    if (int.Parse(i.Tag.ToString()) < int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > int.Parse(_stop.Tag.ToString()))
                        request.Add(i);
            }
            return request;
        }

        private List<Ellipse> PathTwoLine(Ellipse[] allStart, Ellipse[] allStop, int transitionStart, int transitionStop)
        {
            var request = new List<Ellipse>();
            Ellipse[] allElements;
            if (int.Parse(_start.Tag.ToString()) <= transitionStart)
            {
                allElements = allStart.OrderBy(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in allElements)
                    if (int.Parse(i.Tag.ToString()) > int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < transitionStart + 1)
                        request.Add(i);
            }
            if (int.Parse(_start.Tag.ToString()) >= transitionStart)
            {
                allElements = allStart.OrderByDescending(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in allElements)
                    if (int.Parse(i.Tag.ToString()) < int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > transitionStart - 1)
                        request.Add(i);
            }
            if (int.Parse(_stop.Tag.ToString()) <= transitionStop)
            {
                allElements = allStop.OrderByDescending(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in allElements)
                    if (int.Parse(i.Tag.ToString()) > int.Parse(_stop.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < transitionStop + 1)
                        request.Add(i);
            }
            if (int.Parse(_stop.Tag.ToString()) >= transitionStop)
            {
                allElements = allStop.OrderBy(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in allElements)
                    if (int.Parse(i.Tag.ToString()) < int.Parse(_stop.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > transitionStop - 1)
                        request.Add(i);
            }
            return request;
        }

        private void ClickOnStation()
        {
            foreach (var v in _allStations)
            {
                v.MouseDown += delegate
                {
                    if (_start != null && _stop != null)
                        Reset();
                    v.Fill = Brushes.Yellow;
                    if (!Equals(v, _start) && _start == null)
                    {
                        _start = v;
                        Destination.Content = $"From {v.Name}";
                    }
                    else
                    {
                        _stop = v;
                        Destination.Content += $" to {v.Name}";
                    }
                    if (_start != null && _stop != null)
                    {
                        _route = Run(_start, _stop);
                        _timer.Start();
                    }
                };
            }
        }

        private void Reset()
        {
            _timer.Stop();
            _timerColor.Stop();
            _color = true;
            Destination.Content = null;
            foreach (var j in _redLine)
                j.Fill = Brushes.Red;
            foreach (var j in _blueLine)
                j.Fill = Brushes.Blue;
            foreach (var j in _greenLine)
                j.Fill = Brushes.Green;
            _start = null;
            _stop = null;
            _count = 0;
        }

        private void ChangeColor(object sender, EventArgs e)
        {
            if (_color)
            {
                _start.Fill = Brushes.Yellow;
                for (int i = _route.Count - 1; i >= 0; i--)
                {
                    if (_route[i].Name.Length > 3)
                        _route[i].Fill = Brushes.Yellow;
                    if (_route[i].Name.Length <= 3)
                        _route[i].Fill = null;
                }
                _stop.Fill = Brushes.Yellow;
                _color = false;
                return;
            }
            if (!_color)
            {
                _start.Fill = Brushes.Black;
                for (int i = _route.Count - 1; i >= 0; i--)
                    if (_route[i].Name.Length > 3)
                        _route[i].Fill = Brushes.Black;
                _stop.Fill = Brushes.Black;
                _color = true;
            }
        }

        private void Timer(object sender, EventArgs e)
          {
               if (_count == _route.Count)
               {
                   _timer.Stop();
                   _timerColor.Start();
                   if (_start != null)
                   return;
               }
               while (_count < _route.Count)
               {
                _count++;
                   break;
               }
           }
    }
}