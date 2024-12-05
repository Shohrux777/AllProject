@extends("private.maket.skelet")

@section("title") Edit Company   @endsection
@section("content")
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning">
                            <h4 class="card-title">Edit Company</h4>
                            <div class="card-body table-responsive">
                        <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <form action="{{route('cupdate',$data->id)}}" method ="get">
                                @csrf
                                <div class="card">
                                    
                                    <div class="card-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <input class="form-control" name="name" placeholder="Name" value="{{$data->name}}"  style="background-color: rgba(190,172,206,0.33)"/>
                                                <br>
                                                <input type="number" class="form-control" name="number" placeholder="Number"  value="{{$data->number}}" style="background-color: rgba(190,172,206,0.33)"/>
                                                <input type="hidden"   value="{{$data->id}}"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                    
                                        <button type="submit" class="btn btn-primary">Update</button>
                                    </div>
                                </div>
                                <div class="card-body table-responsive">
                                    
                                </div>
                            </div>
                        </div>
                    </div> 
@endsection