@extends("private.maket.skelet")

@section("title") Admin panel @endsection
@section("content")


                        <!-- filter -->
                        <form action="{{route('filter2')}}" method="get">
                            <div  class="flex flex-wrap items-center text-gray-700 border-t-2 border-l-4 border-r-4 border-gray-300" style="display:flex">
                             <div style="width:100%;display:flex;">
                                {{-- @if($from_date != NULL) --}}
                                {{-- <div class="col-md-2">
                                    <h5>{{$from_date}} from date </h5>
                                    <input type="date" name="from_date" id="from_date" class="form-control" value="{{$from_date}}"  placeholder="Tanlangan sanada:"  />
                                </div>
                                <div class="col-md-2">
                                    <h5>{{$to_date}}  to</h5>
                                    <input type="date" name="to_date" id="to_date" class="form-control" value="{{$to_date}}" placeholder="Gacha:"  />
                                </div> --}}
                                <div class="col-md-2">
                                    {{-- <h5> <?php echo date('d-M-Y'); ?> </h5> --}}
                                    <input type="date" name="from_date" id="from_date" value="{{$from_date}}" class="form-control mt-2" placeholder="Tanlangan sanada:"  />
                                </div>
                                <div class="col-md-2">
                                    {{-- <h5> this day </h5> --}}
                                    <input type="date" name="to_date" id="to_date" value="{{$to_date}}" class="form-control mt-2" placeholder="Gacha:"  />
                                </div>

                                <div class="col-md-2">
                                    <!-- <h5 style="display: flex;"> <img style="width: 20px;height: 20px;" src='print.png' alt='print'>  <input type="button" onclick="printDiv('print')"  value="Chop etish" /> </h5> -->
                                <div style="display: flex;">
                                <button type="submit"  class="btn btn-info">Поиск</button>
                                <a type="submit" class="btn btn-info" href="{{route('filter2')}}">Обновить</a>
                                </div>
                                </div>

                            </div>
                    </div>

     </form>

     <!-- end filter -->
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card">
                                        <div class="card-header " style="background:#1B3444">
                                            <h4 class="card-title text-white">Действия сотрудников</h4>

                                        </div>
                                        <div class="card-body table-responsive">
                                            <table id="myTable" class="table table-hover">
                                                <thead class="text-warning">
                                                    <th>ФИО</th>
                                                    <th>Время</th>
                                                    <th>Дверь</th>
                                                </thead>
                                                <tbody>
                                                @if(count($datas) > 0)

                                                @for($i = 0;$i< count($datas);$i++)
                                                <tr>
                                                    <td>{{$datas[$i]->personName}}</td>

                                                    <!-- <td>{{$datas[$i]->authDateTime}}</td> -->
                                                    <td>{{date_format(date_create($datas[$i]->authDateTime),"Y-m-d H:i:s") }}</td>

                                                    <td>{{$datas[$i]->deviceName}}</td>

                                                </tr>
                                                @endfor
                                                @else
                                                    <tr>
                                                        <td class="text-center" colspan="5">Результаты не найдены</td>
                                                    </tr>
                                                @endif

                                                </tbody>
                                                {{-- <tfoot style="text-align: left;">
                                                    <tr>
                                                        <th colspan='1'>Summary:</th>
                                                        <th id="total"></th>
                                                    </tr>
                                                </tfoot> --}}
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
        </div>
    </div>

@endsection
