using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    
    class Painter
    {
        public Color mainColor = Color.Black; //Цвет рисования
        public Color backgroundColor = Color.White; //Цвет фона
        public Point lastPoint, newPoint,NCS; //lastPoint - последняя нажатая точка, newPoint -новая точка, при движении обновляется
        // NCS - точка для удобста, переводит систему координат panel в декартому систему
        public Rectangle rec;
        public bool IsPressed = false; //Наката ли кнопка шифт
        public int sizePen = 5; // размер ручки
        public int selection = 1; // выбор инстумента
        public Size sizeEarse = new Size(50, 50); //размер ластика
        public Font font = new Font("Times New Roman", 25, FontStyle.Regular); //свойства текста
        public string text; // текст
        public double alpha; // синус угла, для того, чтобы поворачивать линию на 45 градусов
        public bool IsPressedButton = false;

        private Image img, img2;
        private Image image;
        private Graphics imageG;
        private Graphics ig;
        private Pen pen;
        private Brush brush;
        private BufferedGraphics bg;
        private Graphics MainGraphics;

        public Painter(Graphics g)
        {
            MainGraphics = g;
            img = new Bitmap((int)MainGraphics.VisibleClipBounds.Width,
                             (int)MainGraphics.VisibleClipBounds.Height,
                             MainGraphics);
            ig = Graphics.FromImage(img);
            bg = BufferedGraphicsManager.Current.Allocate(g, Rectangle.Round(g.VisibleClipBounds));
            
        }

        public void TurnAround() // поворот на
        {
            if (NCS.X > 0 && NCS.Y > 0 && (alpha < 0.5 && alpha >= 0)) newPoint.Y = lastPoint.Y;
            if (NCS.X > 0 && NCS.Y > 0 && (alpha > 0.5 && alpha < 0.8))
            {
                newPoint.X = lastPoint.X + (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
                newPoint.Y = lastPoint.Y - (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
            }
            if (NCS.X > 0 && NCS.Y > 0 && (alpha > 0.8 && alpha <= 1)) newPoint.X = lastPoint.X;
            if (NCS.X < 0 && NCS.Y > 0 && (alpha < 0.5 && alpha >= 0)) newPoint.Y = lastPoint.Y;
            if (NCS.X < 0 && NCS.Y > 0 && (alpha > 0.5 && alpha < 0.8))
            {
                newPoint.X = lastPoint.X - (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
                newPoint.Y = lastPoint.Y - (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
            }
            if (NCS.X < 0 && NCS.Y > 0 && (alpha > 0.8 && alpha <= 1)) newPoint.X = lastPoint.X;
            if (NCS.X < 0 && NCS.Y < 0 && (alpha < 0.5 && alpha >= 0)) newPoint.Y = lastPoint.Y;
            if (NCS.X < 0 && NCS.Y < 0 && (alpha > 0.5 && alpha < 0.8))
            {
                newPoint.X = lastPoint.X - (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
                newPoint.Y = lastPoint.Y + (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
            }
            if (NCS.X < 0 && NCS.Y < 0 && (alpha > 0.8 && alpha <= 1)) newPoint.X = lastPoint.X;
            if (NCS.X > 0 && NCS.Y < 0 && (alpha < 0.5 && alpha >= 0)) newPoint.Y = lastPoint.Y;
            if (NCS.X > 0 && NCS.Y < 0 && (alpha > 0.5 && alpha < 0.8))
            {
                newPoint.X = lastPoint.X + (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
                newPoint.Y = lastPoint.Y + (int)(Distance(newPoint, lastPoint) * Math.Sin(Math.PI / 4));
            }
            if (NCS.X > 0 && NCS.Y < 0 && (alpha > 0.8 && alpha <= 1)) newPoint.X = lastPoint.X;
        }



        public double Distance(Point a,Point b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        } //Расстояние между точками 
        
        public void PaintPen(Graphics g) //Рисуем ручкой
        {            
            pen = new Pen(mainColor, sizePen); //Создаем ручку
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round; //Закругляем концы и начало
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //Сглаживаем графикс
            g.DrawLine(pen, lastPoint, newPoint); //Метод рисования линии
            lastPoint = newPoint;        //Обновляем точку, чтобы рисовать непрерывно
        }

        public void PaintEraser(Graphics g)//Рисуем ластиком
        {        
            brush = new SolidBrush(backgroundColor); //Заливка
            Rectangle rec = new Rectangle(lastPoint.X- sizeEarse.Width/2,lastPoint.Y- sizeEarse.Height/2,//Создам прямоугольник
                                          sizeEarse.Width, sizeEarse.Height); //Описанный вокруг эллипса
            g.FillEllipse(brush,rec); //Рисуем залитый эллипс
            lastPoint = newPoint; //Обносляем точку, чтобы рисовать непрерывно
        } 

        public void PaintLine(Graphics g,bool key) //Рисуем линией
        {
            pen = new Pen(mainColor, sizePen); //Создаем ручку
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round; //Закругляем концы и начало
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //Сглаживаем графикс
            if (key) //Если нажата shift
            {              
                Point l = new Point(lastPoint.X, newPoint.Y); //Новая точка point перпендикулярная lastpoint
                alpha = (Math.Sin(Math.Abs(Distance(lastPoint,l)/Distance(lastPoint,newPoint)))); //Вычисление угла
                TurnAround();
                g.DrawLine(pen, lastPoint, newPoint);
            }
            else
            {
                g.DrawLine(pen, lastPoint, newPoint);
            }
        }
        
        /*
           Такой же метод как и рисование линии
           Только конец преобразуем не в круглое а в стрелку
        */
        public void PaintLineArrow(Graphics g, bool key)
        {
            pen = new Pen(mainColor, sizePen);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (key)
            {
                Point l = new Point(lastPoint.X, newPoint.Y);
                alpha = (Math.Sin(Math.Abs(Distance(lastPoint, l) / Distance(lastPoint, newPoint))));
                TurnAround();
                g.DrawLine(pen, lastPoint, newPoint);
            }
            else
            {
                g.DrawLine(pen, lastPoint, newPoint);
            }
        }

        public void PaintCircle(Graphics g)
        {
            pen = new Pen(mainColor, sizePen);
            pen.EndCap  = pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            /*
             Если мы находимся по координате X справа в декартовой системе координат, то рисуем эллипс как обычно,
             Если мы находимся по Y положительно, то от посоледней точки мы отнимаем наш NCS
             Если мы находимся в отрицательном X то двигаем стартовую точку назад и начинаем рисовать от нее
             Если мы находимся в отрицательном Y то размеры наши меняем на -;
             */
             
            rec = new Rectangle(NCS.X > 0 ? lastPoint.X : lastPoint.X + NCS.X,
                                   NCS.Y > 0 ? lastPoint.Y - NCS.Y : lastPoint.Y,
                                   NCS.X > 0 ? NCS.X : -NCS.X,
                                   NCS.Y > 0 ? NCS.Y : -NCS.Y);
            g.DrawEllipse(pen, rec);      
        }

        /*
         Тоже самое что и предыдуший метод
         */
        public void PaintRectangle(Graphics g)
        {
            pen = new Pen(mainColor, sizePen);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rec = new Rectangle(NCS.X > 0 ? lastPoint.X : lastPoint.X + NCS.X,
                                   NCS.Y > 0 ? lastPoint.Y - NCS.Y : lastPoint.Y,
                                   NCS.X > 0 ? NCS.X : -NCS.X,
                                   NCS.Y > 0 ? NCS.Y : -NCS.Y);
            if(lastPoint.X == newPoint.X || lastPoint.Y == newPoint.Y)
            {
                g.DrawLine(pen, lastPoint, newPoint);
            }
            else
            {
                g.DrawRectangle(pen, rec);
            }
        }
        /*
         Тоже самое что и предыдуший метод только с заливкой
         */
        public void PaintRectangleFill(Graphics g)
        {
            brush = new SolidBrush(mainColor);
            pen = new Pen(mainColor, sizePen);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rec = new Rectangle(NCS.X > 0 ? lastPoint.X : lastPoint.X + NCS.X,
                                   NCS.Y > 0 ? lastPoint.Y - NCS.Y : lastPoint.Y,
                                   NCS.X > 0 ? NCS.X : -NCS.X,
                                   NCS.Y > 0 ? NCS.Y : -NCS.Y);
            if (lastPoint.X == newPoint.X || lastPoint.Y == newPoint.Y)
            {
                g.DrawLine(pen, lastPoint, newPoint);
            }
            else
            {
                g.FillRectangle(brush, rec);
            }
        }
        /*
         Тоже самое что и предыдуший метод только с заливкой
         */
        public void PaintCircleFill(Graphics g)
        {
            brush = new SolidBrush(mainColor);
            pen = new Pen(mainColor, sizePen);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rec = new Rectangle(NCS.X > 0 ? lastPoint.X : lastPoint.X + NCS.X,
                                   NCS.Y > 0 ? lastPoint.Y - NCS.Y : lastPoint.Y,
                                   NCS.X > 0 ? NCS.X : -NCS.X,
                                   NCS.Y > 0 ? NCS.Y : -NCS.Y);
            if (lastPoint.X == newPoint.X || lastPoint.Y == newPoint.Y)
            {
                g.DrawLine(pen, lastPoint, newPoint);
            }
            else
            {
                g.FillEllipse(brush, rec);
            }
        }

        /*
         Сам метод рисования который мы объявляем в классе Form1 
        */
        public void PaintMouseMove(Point e,bool key)
        {

            if (IsPressed)
            {
                newPoint = e; //Обновляем точку каждый раз когда двигаем мышкой
                NCS = new Point(newPoint.X - lastPoint.X, lastPoint.Y-newPoint.Y); //переход из экранных координат в декартовы
                switch (selection) //Выбор инструмента
                {
                    case 1:
                        bg.Graphics.Clear(backgroundColor); //Очищаем график
                        PaintPen(ig); // рисуем на графике картинки
                        bg.Graphics.DrawImage(img, new Point(0, 0)); //Отрисовываем нашу картинку на буфере
                        bg.Render(MainGraphics); //отрисовываем его на основной график
                        break;
                    case 2:
                        bg.Graphics.Clear(backgroundColor);
                        PaintEraser(ig);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        bg.Render(MainGraphics);
                        break;
                    case 3:
                        bg.Graphics.Clear(backgroundColor);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        PaintLine(bg.Graphics, key);
                        bg.Render(MainGraphics);
                        break;
                    case 5:
                        bg.Graphics.Clear(backgroundColor);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        PaintLineArrow(bg.Graphics, key);
                        bg.Render(MainGraphics);
                        break;
                    case 6:
                        bg.Graphics.Clear(backgroundColor);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        PaintCircle(bg.Graphics);
                        bg.Render(MainGraphics);
                        break;
                    case 7:
                        bg.Graphics.Clear(backgroundColor);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        PaintRectangle(bg.Graphics);
                        bg.Render(MainGraphics);
                        break;
                    case 8:
                        bg.Graphics.Clear(backgroundColor);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        PaintRectangleFill(bg.Graphics);
                        bg.Render(MainGraphics);
                        break;
                    case 9:
                        bg.Graphics.Clear(backgroundColor);
                        bg.Graphics.DrawImage(img, new Point(0, 0));
                        PaintCircleFill(bg.Graphics);
                        bg.Render(MainGraphics);
                        break;
                }
            }
        }

        public void PaintMouseUp(bool key)
        {
            IsPressed = false; // если мышка отжата то false
            switch (selection) // выбор инстумента, если мы отжимаем мышку то мы рисуем окаончательный вариент на главном графике
            {
                case 3:
                    PaintLine(ig,key); // рисуем последний раз на графике картинки
                    bg.Graphics.DrawImage(img, new Point(0, 0)); //Рисуем на баффер графикс 
                    bg.Render(MainGraphics); // отрисовываем последний раз
                    break;
                case 5:
                    PaintLineArrow(ig,key);
                    bg.Graphics.DrawImage(img, new Point(0, 0));
                    bg.Render(MainGraphics);
                    break;
                case 6:
                    PaintCircle(ig);
                    bg.Graphics.DrawImage(img, new Point(0, 0));
                    bg.Render(MainGraphics);
                    break;
                case 7:
                    PaintRectangle(ig);
                    bg.Graphics.DrawImage(img, new Point(0, 0));
                    bg.Render(MainGraphics);
                    break;
                case 8:
                    PaintRectangleFill(ig);
                    bg.Graphics.DrawImage(img, new Point(0, 0));
                    bg.Render(MainGraphics);
                    break;
                case 9:
                    PaintCircleFill(ig);
                    bg.Graphics.DrawImage(img, new Point(0, 0));
                    bg.Render(MainGraphics);
                    break;

            }
        }
        public void KeyPressed(Char e,Panel panel1,TextBox textBox) // Если нажата enter когда пишем текст
        {
            if (e == (char)13) // есил нажата enter
            {
                text = textBox.Text; //сохраняем текст из текстового окна в поле 
                Brush brush = new SolidBrush(mainColor); // создаем заливку
                panel1.Controls.Remove(textBox); // удаляем бокс
                bg.Graphics.Clear(backgroundColor); // очищаем график
                ig.DrawString(text, font, brush, new Point(lastPoint.X - 50,
                                                           lastPoint.Y - 11)); // рисуем текст который писали 
                bg.Graphics.DrawImage(img, new Point(0, 0));      //рисуем картинку на баффер графикс
                bg.Render(MainGraphics); // отрисовываем
                
            }
        }     

        public void LoadImage(Image imgg, Panel panel1) // Загрузка картинкии
        {
            panel1.Size = imgg.Size; // Приравниваем размеры панели к размерам картинки
            backgroundColor = Color.White;
            RefreshGraphicsSize(panel1); // обновляем панель
            img = imgg; //копируем новую картинку 
            ig = Graphics.FromImage(imgg); // обновляем нрафикс на новой картинке
        }

        public void SaveImage(String filename,Panel panel1) // Загрузка картинки
        {
            RefreshGraphicsSize(panel1); // обновляем панель
            image = new Bitmap((int)MainGraphics.VisibleClipBounds.Width,
                             (int)MainGraphics.VisibleClipBounds.Height,
                             MainGraphics);
            imageG = Graphics.FromImage(image);
            imageG.Clear(backgroundColor);
            imageG.DrawImage(img,new Point(0,0));
            image.Save(filename); // созраняем картинку
        }

        public void Paint(Panel panel1) // Отрисовывание когда заходим 
        {
            RefreshGraphicsSize(panel1);
            MainGraphics.DrawImage(img, new Point(0, 0));
        }
        
       
        public void RefreshGraphicsSize(Panel panel1)
        {
            
            img2 = img;
            MainGraphics = panel1.CreateGraphics();
            img = new Bitmap((int)MainGraphics.VisibleClipBounds.Width,
                             (int)MainGraphics.VisibleClipBounds.Height,
                             MainGraphics);
            ig = Graphics.FromImage(img);
            ig.DrawImage(img2, new Point(0,0));           
            bg = BufferedGraphicsManager.Current.Allocate(MainGraphics, Rectangle.Round(MainGraphics.VisibleClipBounds));       
            bg.Graphics.Clear(backgroundColor);
            bg.Graphics.DrawImage(img2, new Point(0, 0));
            bg.Render(MainGraphics);



        }

        
        /*
        public void Rotate()
        {
            ig.ScaleTransform(4, 4);
        }
        */
    }
}
