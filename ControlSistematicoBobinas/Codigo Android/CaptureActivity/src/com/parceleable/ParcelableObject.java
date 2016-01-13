/**
 * 
 */
/**
 * @author Jose Ignacio Castelli
 *
 */
package com.parceleable;

import android.os.Parcel;
import android.os.Parcelable;

public class ParcelableObject implements Parcelable {
	private Object parceleableObject;

	public Object getParceleableObject() {
		return parceleableObject;
	}

	public ParcelableObject(Object objectParam) {
		super();
		parceleableObject = objectParam;
	}

	private ParcelableObject(Parcel in) {
		/*laptop = new Laptop();
		laptop.setId(in.readInt());
		laptop.setBrand(in.readString());
		laptop.setPrice(in.readDouble());
		laptop.setImageBitmap((Bitmap) in.readParcelable(Bitmap.class
				.getClassLoader()));*/
	}

	/*
	 * you can use hashCode() here.
	 */
	@Override
	public int describeContents() {
		return 0;
	}

	/*
	 * Actual object Serialization/flattening happens here. You need to
	 * individually Parcel each property of your object.
	 */
	@Override
	public void writeToParcel(Parcel parcel, int flags) {
		/*parcel.writeInt(laptop.getId());
		parcel.writeString(laptop.getBrand());
		parcel.writeDouble(laptop.getPrice());
		parcel.writeParcelable(laptop.getImageBitmap(),
				PARCELABLE_WRITE_RETURN_VALUE);*/
	}

	/*
	 * Parcelable interface must also have a static field called CREATOR,
	 * which is an object implementing the Parcelable.Creator interface.
	 * Used to un-marshal or de-serialize object from Parcel.
	 */
	public static final Parcelable.Creator<ParcelableObject> CREATOR = 
			new Parcelable.Creator<ParcelableObject>() {
		public ParcelableObject createFromParcel(Parcel in) {
			return new ParcelableObject(in);
		}

		public ParcelableObject[] newArray(int size) {
			return new ParcelableObject[size];
		}
	};
}
