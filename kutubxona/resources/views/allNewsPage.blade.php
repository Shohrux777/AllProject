@extends('layouts.mainLayout')
@section('title', 'Bosh sahifa')
<!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->


@section('content')
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

  </div>
  <!-- News and Btn complected -->

@endsection
