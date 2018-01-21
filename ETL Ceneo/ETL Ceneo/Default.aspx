<!DOCTYPE html>
<html lang="en">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0"/>
  <title>ETL - Ceneo</title>

  <!-- CSS  -->
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection"/>
  <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection"/>
</head>
<body>
  <nav class="light-blue lighten-1" role="navigation">
    <div class="nav-wrapper container"><a id="logo-container" href="#" class="brand-logo"></a>
      <ul class="right hide-on-med-and-down">
        <li><a href="#">Rozpocznij proces dla nowego produktu</a></li>
      </ul>

      <ul id="nav-mobile" class="side-nav">
        <li><a href="#"></a></li>
      </ul>
      <a href="#" data-activates="nav-mobile" class="button-collapse"><i class="material-icons">menu</i></a>
    </div>
  </nav>
  <div class="section no-pad-bot" id="index-banner">
    <div class="container">
      <br><br>
      <h1 class="header center orange-text">Aplikacja ETL</h1>
      <h5 class="header center orange-text">Pobieranie treści z portalu ceneo.pl</h5>
      <br />
	  <div class="input-field col s6">
          <input id="itemId" type="text" class="validate">
          <label for="itemId"><center>Kod produktu</label>
      </div>
		<br /> 
      <div class="row center">
        <a href="index.html" id="download-button" class="btn-large waves-effect waves-light orange">Rozpocznij proces ETL</a>
      </div>
      <br>

    </div>
  </div>

  <div class="container">
    <div class="section">

      <!--   Icon Section   -->
      <div class="row">
        <div class="col s12 m4">
          <div class="icon-block">
            <h2 class="center light-blue-text"><i class="material-icons">flash_on</i></h2>
            <h5 class="center">Extract</h5>

            <p class="light">Rozpocznij podproces Extract, którego zadaniem jest pobranie niezbędnych informacji dla podanego produktu.</p>
          </div>
        </div>

        <div class="col s12 m4">
          <div class="icon-block">
            <h2 class="center light-blue-text"><i class="material-icons">settings</i></h2>
            <h5 class="center">Transform</h5>

            <p class="light">Rozpocznij proces Transform, którego zadaniem jest przekształcenie danych pobranych w poprzednim kroku. Warunkiem jest pomyślne ukończenie podprocesu Extract.</p>
          </div>
        </div>

        <div class="col s12 m4">
          <div class="icon-block">
            <h2 class="center light-blue-text"><i class="material-icons">group</i></h2>
            <h5 class="center">Load</h5>

            <p class="light">Załaduj dane do bazy danych oraz wyświetl je użytkownikowi aplikacji. Warunkiem jest pomyślne ukończenie podprocesów Extract oraz Load.</p>
          </div>
        </div>
      </div>

    </div>
    <br><br>
  </div>

  <footer class="page-footer orange">
    <div class="container">
      <div class="row">
        <div class="col l6 s12">
          <h5 class="white-text">Opis projektu</h5>
          <p class="grey-text text-lighten-4">Aplikacja została wykonana celem zaliczenia przedmiotu Hurtownie Danych na kierunku Informatyka Stosowana, studia magisterskie. <br /></p>


        </div>
		
		 <div class="col l3 s12">
          <h5 class="white-text">Prowadzący:</h5>
          <ul>
            <li><a class="white-text" href="#!"> mgr Katarzynę Wójcik</a></li>

          </ul>
        </div>
		
        <div class="col l3 s12">
          <h5 class="white-text">Autorzy</h5>
          <ul>
            <li>Marcin Leganowski</li>
            <li>Dariusz Majkowski</li>
            <li>Jarosław Mirek</li>
          </ul>
        </div>
       
      </div>
    </div>
    <div class="footer-copyright">
      <div class="container">
      <center>Copyright © 2018 Uniwerytet Ekonomiczny w Krakowie</center>
      </div>
    </div>
  </footer>


  <!--  Scripts-->
  <script src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
  <script src="js/materialize.js"></script>
  <script src="js/init.js"></script>

  </body>
</html>
