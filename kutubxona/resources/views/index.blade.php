@extends('layouts.mainLayout')
@section('title', 'Bosh sahifa')
<!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->


@section('content')
  <div class="container-fluid p-0">
    <!--  Section Stlayder -->
    <section id="hero">
      <div class="hero-container">
        <div id="heroCarousel" class="carousel slide carousel-fade" data-bs-ride="carousel" data-bs-interval="5000">

          <ol id="hero-carousel-indicators" class="carousel-indicators"></ol>

          <div class="carousel-inner" role="listbox">

            <div class="carousel-item active" style="background-image: url(img/4.jpg)">
              <div class="carousel-container">
                <div class="container">
                  <h2 class="animate__animated animate__fadeInDown main_text">Mirzacho'l tuman axborot-kutubxona markazi</h2>
                  <div class="salom">
                    <p class="animate__animated animate__fadeInUp info_text">Bizning kutubxona ko'plab kitoblarni o'z ichiga oladi</p>
                  </div>
                  <!-- <a href="#about" class="btn-get-started scrollto animate__animated animate__fadeInUp">Boshlash</a> -->
                </div>
              </div>
            </div>

            <div class="carousel-item" style="background-image: url(img/3.jpg)">
              <div class="carousel-container">
                <div class="container">
                  <h2 class="animate__animated animate__fadeInDown main_text">Bolalar uchun kitoblar</h2>
                  <div class="salom">
                      <p class="animate__animated animate__fadeInUp">Kutubxonamiz bolalar uchun ko'plab kitoblarni o'z ichiga oladi</p>
                  </div>
                  <!-- <a href="#about" class="btn-get-started scrollto animate__animated animate__fadeInUp">Boshlash</a> -->
                </div>
              </div>
            </div>

            <div class="carousel-item" style="background-image: url(img/5.jpg)">
              <div class="carousel-container">
                <div class="container">
                  <h2 class="animate__animated animate__fadeInDown main_text">Badiiy adabiyotlar to'plami</h2>
                  <div class="salom">
                    <p class="animate__animated animate__fadeInUp">Badiiy va ilmiy asarlarni shu yerdan topishingiz mumkun</p>
                  </div>
                  <!-- <a href="#about" class="btn-get-started scrollto animate__animated animate__fadeInUp">Boshlash</a> -->
                </div>
              </div>
            </div>

          </div>

          <a class="carousel-control-prev" href="#heroCarousel" role="button" data-bs-slide="prev">
            <span class="carousel-control-prev-icon bi bi-chevron-left" aria-hidden="true"></span>
          </a>

          <a class="carousel-control-next" href="#heroCarousel" role="button" data-bs-slide="next">
            <span class="carousel-control-next-icon bi bi-chevron-right" aria-hidden="true"></span>
          </a>

        </div>
      </div>
    </section>
    <!--  Section Stlayder complected -->
  </div>
  <div class="container-fluid">

    <!--  Section News -->
    <div class="news">
      <div class="title">
        <h1>So'nggi yangiliklar</h1>
        <p>Eng so'nggi yangiliklar bilan tanishib boring</p>
        <div class="line_under">
          <div class="d-flex align-items-center">
            <div class="line"></div>
            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-school" width="60" height="60" viewBox="0 0 24 24" stroke-width="2" stroke="rgb(18, 3, 103)" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M22 9l-10 -4l-10 4l10 4l10 -4v6" />
              <path d="M6 10.6v5.4a6 3 0 0 0 12 0v-5.4" />
            </svg>
            <div class="line"></div>
          </div>
        </div>
      </div>
    </div>
    <!--  Section News complected-->

  </div>
  <!-- News and Btn -->
  <div class="container">
      <!--   News Contents  -->
    <div class="row row_contents m-0 p-0 w-100">
        @foreach ($news as $new)
            <div class="col-11 col-sm-10 col-md-6 col-lg-4 col-xl-3">
                <div class="content_box">
                    <a href="{{route('newsId', $new->id)}}" class="w-100">
                        <img src="{{asset('/public/news/picture/' .$new->image)}}" class="rounded img-fluid img_news" alt="" style="width: 100%;">
                    </a>
                <div class="content_text">
                    <a href="{{route('newsId', $new->id)}}">
                        <p>{{$new->name}}</p>
                    </a>
                </div>
                <div class="data_arrow">
                    <div class="data_x">
                        <a href="{{route('newsId', $new->id)}}">
                            <span>{{$new->date}}</span>
                        </a>

                    </div>
                    <a href="{{route('newsId', $new->id)}}">
                        <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-arrow-narrow-right" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.5" stroke="#000000" fill="none" stroke-linecap="round" stroke-linejoin="round">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                            <line x1="5" y1="12" x2="19" y2="12" />
                            <line x1="15" y1="16" x2="19" y2="12" />
                            <line x1="15" y1="8" x2="19" y2="12" />
                        </svg>
                    </a>

                </div>
                </div>
            </div>
        @endforeach

    </div>

    <div class="row row_contents m-0 p-0 w-100">
      <div class="col-12">
        <div class="btn_box">
          <button class="send_btn">
            Barcha Yangiliklar
          </button>
        </div>
      </div>
    </div>

  </div>
  <!-- News and Btn complected -->
  <div>
    <!-- ======= Counts Section ======= -->
    <section id="counts" class="counts">
      <div class="count_back d-flex align-items-center">
      <div class="container-fluid">
        <div class="row counters p-0 m-0 w-100">
          <div class="col-7 col-sm-6 col-md-4 col-lg-3 text-center">
            <div class="text_content">
                <div class="box_icon">
                  <i class='bx bxs-user bx-fade-up'></i>
                </div>
                <div class="count_box">
                  <span data-purecounter-start="0" data-purecounter-end="100" data-purecounter-duration="1" class="purecounter"></span>
                </div>
                <div class="text_boxx">
                  <h3>Users</h3>
                </div>
            </div>
          </div>

          <div class="col-7 col-sm-6 col-md-4 col-lg-3 text-center">
            <div class="text_content">
                <div class="box_icon">
                  <i class='bx bxs-book bx-tada'></i>
                </div>
                <div class="count_box">
                  <span data-purecounter-start="0" data-purecounter-end="101" data-purecounter-duration="1" class="purecounter"></span>
                </div>
                <div class="text_boxx">
                  <h3>E resources</h3>
                </div>
            </div>
          </div>

          <div class="col-7 col-sm-6 col-md-4 col-lg-3 text-center">
            <div class="text_content">
                <div class="box_icon">
                  <i class='bx bxs-heart bx-burst'></i>
                </div>
                <div class="count_box">
                  <span data-purecounter-start="0" data-purecounter-end="21" data-purecounter-duration="1" class="purecounter"></span>
                </div>
                <div class="text_boxx">
                  <h3>Liked books</h3>
                </div>
            </div>
          </div>

          <div class="col-7 col-sm-6 col-md-4 col-lg-3 text-center">
            <div class="text_content">
                <div class="box_icon">
                  <i class='bx bx-list-ul bx-flashing'></i>
                </div>
                <div class="count_box">
                  <span data-purecounter-start="0" data-purecounter-end="128" data-purecounter-duration="1" class="purecounter"></span>
                </div>
                <div class="text_boxx">
                  <h3>Fond AKM</h3>
                </div>
            </div>
          </div>

          <!-- <div class="col-lg-3 col-sm-6 col-md-6 col-12 text-center">
            <p>Projects</p>
            <span data-purecounter-start="0" data-purecounter-end="101" data-purecounter-duration="1" class="purecounter"></span>
          </div>

          <div class="col-lg-3 col-sm-6 col-md-6 col-12 text-center">
            <p>Hours Of Support</p>
            <span data-purecounter-start="0" data-purecounter-end="1463" data-purecounter-duration="1" class="purecounter"></span>
          </div>

          <div class="col-lg-3 col-sm-6 col-md-6 col-12 text-center">
            <p>Hard Workers</p>
            <span data-purecounter-start="0" data-purecounter-end="15" data-purecounter-duration="1" class="purecounter"></span>
          </div> -->

        </div>

        </div>
      </div>

    </section>
    <!-- Counts Section complected-->

    <div class="container" id="books">
      <div class="news">
        <div class="title">
          <h1>Kitoblar</h1>
          <p>Kutubxonamizda barcha turdagi kitoblarni va darsliklarni topishingiz mumkun</p>
          <div class="line_under">
            <div class="d-flex align-items-center">
              <div class="line"></div>
              <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-school" width="60" height="60" viewBox="0 0 24 24" stroke-width="2" stroke="rgb(18, 3, 103)" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M22 9l-10 -4l-10 4l10 4l10 -4v6" />
                <path d="M6 10.6v5.4a6 3 0 0 0 12 0v-5.4" />
              </svg>
              <div class="line"></div>
            </div>
          </div>
        </div>
      </div>
    </div>

  </div>
  <!-- Main Books container -->
  <div class="books news container">
      <div class="row row_contents p-0 m-0 w-100">
        <div class="col-12">
          <div class="category-item">
            @foreach ($categories as $category)
                <a href="{{route('categoryId', $category->id)}}"> {{$category->name}}</a>
            @endforeach
          </div>
        </div>
      </div>

      <div class="row row_contents books_items p-0 m-0 mt-4 w-100">
        @foreach ($books as $book)
            <div class="books_item col-10 col-sm-10 col-md-4 col-lg-3">
                <div class="note_item">
                    <a href="{{route('bookId', $book->id)}}" class="w-100 d-flex justify-content-center">
                        <img src="{{asset('/public/images/picture/' .$book->image) }}" class="img-fluid rounded shadow" style="width: 90%;">
                    </a>
                    <div class="tex_boxx">
                        <a href="{{route('bookId', $book->id)}}"><p>{{$book->name}}</p></a>
                        <a href="{{route('bookId', $book->id)}}"><h5>{{$book->writer}}</h5></a>
                    </div>
                </div>
            </div>
        @endforeach
      </div>
  </div>

  <!-- Form Section -->
  <div class="all_x" id="form">
    <div class="container">
      <div class="row row_form m-0 p-0 w-100">
        <div class="col-10 col-sm-10 col-md-8 col-lg-6 col-xl-6">
          <div  class="form_box">
                @if(Session::has('alert-success'))
                    <div class="alert alert-success" role="alert">
                        {{Session::get('alert-success')}}
                    </div>
                @endif
                @if(Session::has('alert-info'))
                    <div class="alert alert-info" role="alert">
                        {{Session::get('alert-info')}}
                    </div>
                @endif
                @if(Session::has('server_error'))
                    <div class="alert alert-danger" role="alert">
                        {{Session::get('server_error')}}
                    </div>
                @endif
                @if ($errors->any())
                    <div class="alert alert-danger">
                        <ul>
                            @foreach ($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                @endif
              <div class="form_title">
                <h4>Kitob buyurtma bering</h4>
              </div>
              <form class="form_action" method="post" action="{{route('mainstore')}}">
                @csrf
                    <div class="form_input">
                        <input type="text" name="name" placeholder="F.I.O">
                    </div>
                    <div class="form_input">
                        <input id="phone" name="tel" type="text" placeholder="+998(__)-___-__-__" data-mask="+000(00)-000-00-00">
                    </div>
                    <div class="form_input">
                        <input type="text" name="b_name" placeholder="Kitob nomi">
                    </div>
                    <div class="form_input">
                        <input type="text" name="creator" placeholder="Muallif(lar)">
                    </div>
                    <div class="form_input">
                        <input type="text" name="year" placeholder="Nashr etilgan yili" maxlength="4" data-mask="0000">
                    </div>
                    <div class="form_btn">
                        <button type="submit">Buyurtma berish</button>
                    </div>
                </form>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- Elonlar container -->
  <div class="container">
    <div class="notes news mb-5">
        <div class="title">
          <h1>E'lonlar</h1>
          <p>Barcha yangiliklardan xabardor bo'lib boring!</p>
          <div class="line_under">
            <div class="d-flex align-items-center">
              <div class="line"></div>
              <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-school" width="32" height="32" viewBox="0 0 24 24" stroke-width="2" stroke="rgb(18, 3, 103)" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M22 9l-10 -4l-10 4l10 4l10 -4v6" />
                <path d="M6 10.6v5.4a6 3 0 0 0 12 0v-5.4" />
              </svg>
              <div class="line"></div>
            </div>
          </div>
        </div>

        <div class="note_main container">
          <div class="row elonlar_rows p-0 m-0 w-100">
            @foreach ($notes as $note)
            <div class="col-10 col-sm-10 col-md-6 col-lg-4 col-xl-3 px-2">
              <div class="note_item">
                <a href="{{route('noteId', $note->id)}}" class="w-100 d-flex justify-content-center">
                    <img src="{{asset('/public/news/picture/' .$note->image) }}" class="img-fluid rounded shadow" style="width: 90%;">
                </a>
                <div class="d-flex box_title">
                  <div class="elon_title1">
                    <div class="main_title1">
                        <a href="{{route('noteId', $note->id)}}">
                            <h4>{{$note->menu}}</h4>
                        </a>
                    </div>
                    <div class="icon_clock">
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-calendar-due" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                <path d="M4 5m0 2a2 2 0 0 1 2 -2h12a2 2 0 0 1 2 2v12a2 2 0 0 1 -2 2h-12a2 2 0 0 1 -2 -2z" />
                                <path d="M16 3v4" />
                                <path d="M8 3v4" />
                                <path d="M4 11h16" />
                                <path d="M12 16m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0" />
                              </svg>
                            <div class="data_clock" style="margin-top:2px;">
                              <h5>{{$note->datestr}}</h5>
                            </div>
                        </div>

                        <div class="d-flex mx-2">
                            <div class="class_clock">
                                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clock" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#14BDEE" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                  <circle cx="12" cy="12" r="9" />
                                  <polyline points="12 7 12 12 15 15" />
                                </svg>
                            </div>
                            <div class="data_clock" style="margin-top:2px;">
                              <h5>{{$note->timestr}}</h5>
                            </div>
                        </div>
                    </div>
                    <div class="text_elon">
                        <a href="{{route('noteId', $note->id)}}">
                            <p>{{$note->text}}</p>
                        </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            @endforeach
          </div>
        </div>
    </div>
  </div>
  <!-- Elonlar container complected-->
@endsection
