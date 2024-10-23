@extends('layouts.mainLayout')
@section('title', 'Bosh sahifa')
<!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->


@section('content')
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
                    <div class="text_elon" style=" width: 100%; height: 45px !important; overflow-y: hidden;">
                        <a href="{{route('noteId', $note->id)}}">
                            <p style="margin: 0; padding: 0; font-size: 14px; color: rgb(98, 98, 98); font-weight: 500;">
                                {{$note->text}}</p>
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
