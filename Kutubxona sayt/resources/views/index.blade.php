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

            <div class="carousel-item active" style="background-image: url(img/hero-carousel/4.jpg)">
              <div class="carousel-container">
                <div class="container">
                  <h2 class="animate__animated animate__fadeInDown main_text">Mirzachol kutubxonasiga xush kelibsiz</h2>
                  <p class="animate__animated animate__fadeInUp">Bizning kutubxona ko'plab kitoblarni o'z ichiga oladi</p>
                  <a href="#about" class="btn-get-started scrollto animate__animated animate__fadeInUp">Boshlash</a>
                </div>
              </div>
            </div>

            <div class="carousel-item" style="background-image: url(img/hero-carousel/5.jpg)">
              <div class="carousel-container">
                <div class="container">
                  <h2 class="animate__animated animate__fadeInDown main_text">Bolalar uchun kitoblar</h2>
                  <p class="animate__animated animate__fadeInUp">Kutubxonamiz bolalar uchun ko'plab kitoblarni o'z ichiga oladi</p>
                  <a href="#about" class="btn-get-started scrollto animate__animated animate__fadeInUp">Boshlash</a>
                </div>
              </div>
            </div>

            <div class="carousel-item" style="background-image: url(img/hero-carousel/3.jpg)">
              <div class="carousel-container">
                <div class="container">
                  <h2 class="animate__animated animate__fadeInDown main_text">Badiy adabiyotlar tuplami</h2>
                  <p class="animate__animated animate__fadeInUp">Badiy va ilmiy a'sarlarni shu yerdan topishingiz mumkun</p>
                  <a href="#about" class="btn-get-started scrollto animate__animated animate__fadeInUp">Boshlash</a>
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
        <h1>So'ngi yangiliklar</h1>
        <p>Eng so'ngi yangiliklar bilan tanishib boring</p>
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
            <div class="col-11 col-sm-6 col-md-4 col-lg-3">
                <div class="content_box">
                    <a href="{{route('newsId', $new->id)}}" class="w-100">
                        <img src="{{asset('/public/news/picture/' .$new->image)}}" class="img-fluid img_news" alt="" style="width: 100%;">
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

    <div class="container">
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
            <div class="books_item col-11 col-sm-6 col-md-4 col-lg-3">
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
  <div class="all_x">
    <div class="container">
      <div class="row row_form m-0 p-0 w-100">
        <div class="col-12 col-sm-11 col-md-10 col-lg-8 col-xl-6">
          <div class="form_box">
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
          <h1>Elonlar</h1>
          <p>Barcha yangiliklardan habardor bo'lib boring!</p>
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
          <div class="row">
            <div class="col-12 col-sm-12 col-md-6 col-lg-4 col-xl-3 px-2">
              <div class="note_item">
                <img src="http://tdpu.uz/upload/7697_21078.jpg" class="img-fluid rounded" style="width: 100%;">
                <div class="d-flex box_title">
                  <div class="data_box">
                    <div class="data1">
                      <h1>25</h1>
                    </div>
                    <div class="data2">
                      <h2>Apr</h2>
                    </div>
                  </div>
                  <div class="elon_title1">
                    <div class="main_title1">
                      <h4>Universitet rektorining abiturientlarga murojaati</h4>
                    </div>
                    <div class="icon_clock">
                        <div class="class_clock">
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clock " width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#14BDEE" fill="none" stroke-linecap="round" stroke-linejoin="round">
                              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                              <circle cx="12" cy="12" r="9" />
                              <polyline points="12 7 12 12 15 15" />
                            </svg>
                        </div>
                        <div class="data_clock">
                          <h5>15.00 - 19.30</h5>
                        </div>
                    </div>
                    <div class="text_elon">
                      <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Iusto, quo.</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-12 col-sm-12 col-md-6 col-lg-4 col-xl-3 px-2">
              <div class="note_item">
                <img src="https://storage.kun.uz/source/9/QgCyRYEs8d3v0R_MADLc8n1-hAlxk1qO.jpg" class="img-fluid rounded" style="width: 100%;">
                <div class="d-flex box_title">
                  <div class="data_box">
                    <div class="data1">
                      <h1>25</h1>
                    </div>
                    <div class="data2">
                      <h2>Apr</h2>
                    </div>
                  </div>
                  <div class="elon_title1">
                    <div class="main_title1">
                      <h4>Universitet rektorining abiturientlarga murojaati</h4>
                    </div>
                    <div class="icon_clock">
                        <div class="class_clock">
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clock " width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#14BDEE" fill="none" stroke-linecap="round" stroke-linejoin="round">
                              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                              <circle cx="12" cy="12" r="9" />
                              <polyline points="12 7 12 12 15 15" />
                            </svg>
                        </div>
                        <div class="data_clock">
                          <h5>15.00 - 19.30</h5>
                        </div>
                    </div>
                    <div class="text_elon">
                      <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Iusto, quo.</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-12 col-sm-12 col-md-6 col-lg-4 col-xl-3 px-2">
              <div class="note_item">
                <img src="https://storage.kun.uz/source/9/eO-xPrv9pr0krQoQ9OM7wGha3pLAlgBh.jpg" class="img-fluid rounded" style="width: 100%;">
                <div class="d-flex box_title">
                  <div class="data_box">
                    <div class="data1">
                      <h1>25</h1>
                    </div>
                    <div class="data2">
                      <h2>Apr</h2>
                    </div>
                  </div>
                  <div class="elon_title1">
                    <div class="main_title1">
                      <h4>Universitet rektorining abiturientlarga murojaati</h4>
                    </div>
                    <div class="icon_clock">
                        <div class="class_clock">
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clock " width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#14BDEE" fill="none" stroke-linecap="round" stroke-linejoin="round">
                              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                              <circle cx="12" cy="12" r="9" />
                              <polyline points="12 7 12 12 15 15" />
                            </svg>
                        </div>
                        <div class="data_clock">
                          <h5>15.00 - 19.30</h5>
                        </div>
                    </div>
                    <div class="text_elon">
                      <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Iusto, quo.</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-12 col-sm-12 col-md-6 col-lg-4 col-xl-3 px-2">
              <div class="note_item">
                <img src="https://storage.kun.uz/source/9/Bn1ybAeCzmF21TMjWwNX07pKbQG4yV61.jpg" class="img-fluid rounded" style="width: 100%;">
                <div class="d-flex box_title">
                  <div class="data_box">
                    <div class="data1">
                      <h1>25</h1>
                    </div>
                    <div class="data2">
                      <h2>Apr</h2>
                    </div>
                  </div>
                  <div class="elon_title1">
                    <div class="main_title1">
                      <h4>Universitet rektorining abiturientlarga murojaati</h4>
                    </div>
                    <div class="icon_clock">
                        <div class="class_clock">
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clock " width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#14BDEE" fill="none" stroke-linecap="round" stroke-linejoin="round">
                              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                              <circle cx="12" cy="12" r="9" />
                              <polyline points="12 7 12 12 15 15" />
                            </svg>
                        </div>
                        <div class="data_clock">
                          <h5>15.00 - 19.30</h5>
                        </div>
                    </div>
                    <div class="text_elon">
                      <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Iusto, quo.</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    </div>
  </div>
  <!-- Elonlar container complected-->
@endsection