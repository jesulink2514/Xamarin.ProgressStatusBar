![](https://somostechies.com/content/images/2017/05/screenshot-1.jpg)
Con la intencion de diseñar una pantalla he construido un control para **Xamarin.Forms** usando **NControl**. Este control nos permite mostrar una barra de progreso que podemos usar para graficar el estado de una orden o algo similar.

[![](https://somostechies.com/content/images/2017/05/2017-05-14-15_59_07-NuGet-Gallery-_-ProgressStatusBar-for-Xamarin-Forms-1-0-0.png)
](https://www.nuget.org/packages/SomosTechies.ProgressStatusBar/)

![](https://somostechies.com/content/images/2017/05/DemoControl.gif)

Los cambios de estados usan el soporte de animaciones de Xamarin.Forms para animar la transicion entre los estados.

     var an = new Animation((d) =>
     {
        this._percentage = d;
        Invalidate();
     },0,1);
     SetValue(CurrentStatusIndexProperty, value);
     an.Commit(this, "percentage",easing:Easing.CubicInOut, length: 500);
     Invalidate();

###Propiedades disponibles

* **StatusesNumber(int)** ,el numero de estados que muestra el control.

* **CurrentStatusIndex (int)**, el indice del estado actual, el indice inicia en 0. 

* **ActiveColor (Xamarin.Forms.Color)**, el color activo de la barra de progreso.

* **InactiveColor (Xamarin.Forms.Color)**, el color inactivo de la barra de progreso.

* **ActiveStatusColor (Xamarin.Forms.Color)**, el color del circulo que representa cada estado cuando este activo.

* **InactiveStatusColor (Xamarin.Forms.Color)**, el color del circulo que representa el estado cuando esta inactivo.

* **StatusBarHeight (double)**, alto de la barra de progreso.

* **StatusCircleRadius (double)**, radio del circulo que representa el estado.
 
Por ejemplo,

    <somostechies:ProgressBarStatusControl x:Name="Status" HeightRequest="15" VerticalOptions="Start" StatusesNumber="4" CurrentStatusIndex="0" HorizontalOptions="FillAndExpand" BackgroundColor="#E4E1E1" ActiveColor="#1DB623" InactiveColor="#9D9D9D" ActiveStatusColor="#0C9B11" InactiveStatusColor="#887E7E"                                      StatusBarHeight="5" StatusCircleRadius="10" Padding="15,10"/>

###Demo
En github se incluye el codigo de la siguiente pantalla de ejemplo. https://github.com/jesulink2514/Xamarin.ProgressStatusBar

![](https://somostechies.com/content/images/2017/05/screenshot.jpg)
