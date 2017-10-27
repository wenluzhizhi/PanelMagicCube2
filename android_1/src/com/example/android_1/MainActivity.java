package com.example.android_1;

import java.io.File;

import android.R.string;
import android.net.Uri;

import android.content.Intent;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import android.content.ContentResolver; 
import android.database.Cursor;
public class MainActivity extends UnityPlayerActivity {

	 private static final int PHOTO_REQUEST_CODE = 1;//相册
	 private static final int PHOTOHRAPH = 2;//相册
	 public static final int NONE = 0;
	 private String myTag="12345"; 
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
    public   void   takePhoto() 
    {
    	Log.d(myTag,"打开相册....");
    	Intent intent=new Intent(Intent.ACTION_PICK,android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
    	startActivityForResult(intent,PHOTO_REQUEST_CODE);
    	Log.d(myTag,"打开相册成功...");
		
	}
    
    public void takeCamera()
    {  
        
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);  
        intent.putExtra(MediaStore.EXTRA_OUTPUT, Uri.fromFile(new File(Environment.getExternalStorageDirectory(), "temp.jpg")));  
        startActivityForResult(intent, PHOTOHRAPH);  
         
   }  
    
    
    
    
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data)
    {
    	if(resultCode==NONE){
    		Log.d(myTag,"resultCode==0");
    		return;
    	}
    	if(requestCode==PHOTO_REQUEST_CODE)
    	{
    		Uri sleUrl=data.getData();
    		String str=getImagePath(sleUrl);
    		UnityPlayer.UnitySendMessage("SelectPage","getPath",str);
    		Log.d(myTag,"向U3d发送数据...");
    	}
    	if (requestCode == PHOTOHRAPH) 
    	{  
            String path = Environment.getExternalStorageDirectory() + "/temp.jpg";  
             
             Log.d("path:", path);  
             
            //调用unity中方法 GetTakeImagePath（path）  
            UnityPlayer.UnitySendMessage("SelectPage", "GetTakeImagePath", path);  
            
        }    
		
	}
    
    
    private String getImagePath(Uri uri)  
    {  
        if(null == uri) return null;  
        String path = null;  
        final String scheme = uri.getScheme();  
        if (null == scheme) {  
            path = uri.getPath();  
        } else if (ContentResolver.SCHEME_FILE.equals(scheme)) {  
            path = uri.getPath();  
        } else if (ContentResolver.SCHEME_CONTENT.equals(scheme)) {  
            String[] proj = { MediaStore.Images.Media.DATA };  
            Cursor cursor = getContentResolver().query(uri, proj, null, null,  
                    null);  
            int nPhotoColumn = cursor  
                    .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);  
            if (null != cursor) {  
                cursor.moveToFirst();  
                path = cursor.getString(nPhotoColumn);  
            }  
            cursor.close();  
        }  
        return path;  
    }  
    
}
