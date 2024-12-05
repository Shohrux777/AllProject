@extends("private.maket.skelet")

@section("title") Create Otdel  @endsection
@section("content")
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning">
                            <h4 class="card-title">Create Otdel</h4>

                        </div>
                        <div class="card-body table-responsive">
                        <div class="row">
                <div class="col-lg-12 col-md-12">
                    <form action="{{route('otdel.store')}}" method="post">
                        @csrf
                        <div class="card">
                            
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="form-group">
                                        <input class="form-control" name="name" placeholder="Name"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <select class="form-control" name="company_name"  style="background-color: rgba(190,172,206,0.33)">
                                        <option>--- Change Company ---</option>
                                        @foreach($company as $c)
                                        <option>{{$c->name}}</option>
                                        @endforeach
                                        </select>
                                        <br>
                                        <input class="form-control" type="number" name="number" placeholder="Number"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <textarea class="form-control"  style="background-color: rgba(190,172,206,0.33)" name="string" id="" placeholder="Bio" cols="5" rows="5"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                              
                                <button type="submit" class="btn btn-primary">Create</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
                        </div>
                    </div>
                </div>
            </div> 
@endsection