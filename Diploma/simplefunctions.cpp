#include "simplefunctions.h"

double sf1(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*r*(0.5 - Power(Cos(th),2)/2);
	}

 double sf2(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(Cos(th)/2 - Power(Cos(th),3)/2);
	}

 double sf3(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(0.5 - Power(Cos(th),2)/2))/r;
	}

 double sf4(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(-0.125 + (3*Power(Cos(th),2))/4 - (5*Power(Cos(th),4))/8))/r;
	}

 double sf5(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,2)*(0.5 - Power(Cos(th),2)/2);
	}

 double sf6(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,4)*(0.5 - Power(Cos(th),2)/2);
	}

 double sf7(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(Cos(th)/2 - Power(Cos(th),3)/2))/Power(r,2);
	}

 double sf8(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*((-3*Cos(th))/8 + (5*Power(Cos(th),3))/4 - (7*Power(Cos(th),5))/8))/Power(r,2);
	}

 double sf9(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,3)*(Cos(th)/2 - Power(Cos(th),3)/2);
	}

 double sf10(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,5)*(Cos(th)/2 - Power(Cos(th),3)/2);
	}

 double sf11(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(-0.125 + (3*Power(Cos(th),2))/4 - (5*Power(Cos(th),4))/8))/Power(r,3);
	}

 double sf12(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(0.0625 - (15*Power(Cos(th),2))/16 + (35*Power(Cos(th),4))/16 - (21*Power(Cos(th),6))/16))/Power(r,3);
	}

 double sf13(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,4)*(-0.125 + (3*Power(Cos(th),2))/4 - (5*Power(Cos(th),4))/8);
	}

 double sf14(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,6)*(-0.125 + (3*Power(Cos(th),2))/4 - (5*Power(Cos(th),4))/8);
	}

 double sf15(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*((-3*Cos(th))/8 + (5*Power(Cos(th),3))/4 - (7*Power(Cos(th),5))/8))/Power(r,4);
	}

 double sf16(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*((5*Cos(th))/16 - (35*Power(Cos(th),3))/16 + (63*Power(Cos(th),5))/16 - (33*Power(Cos(th),7))/16))/Power(r,4);
	}

 double sf17(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,5)*((-3*Cos(th))/8 + (5*Power(Cos(th),3))/4 - (7*Power(Cos(th),5))/8);
	}

 double sf18(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,7)*((-3*Cos(th))/8 + (5*Power(Cos(th),3))/4 - (7*Power(Cos(th),5))/8);
	}

 double sf19(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(0.0625 - (15*Power(Cos(th),2))/16 + (35*Power(Cos(th),4))/16 - (21*Power(Cos(th),6))/16))/Power(r,5);
	}

 double sf20(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(-0.0390625 + (35*Power(Cos(th),2))/32 - (315*Power(Cos(th),4))/64 + (231*Power(Cos(th),6))/32 - (429*Power(Cos(th),8))/128))/Power(r,5);
	}

 double sf21(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,6)*(0.0625 - (15*Power(Cos(th),2))/16 + (35*Power(Cos(th),4))/16 - (21*Power(Cos(th),6))/16);
	}

 double sf22(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,8)*(0.0625 - (15*Power(Cos(th),2))/16 + (35*Power(Cos(th),4))/16 - (21*Power(Cos(th),6))/16);
	}

 double sf23(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*((5*Cos(th))/16 - (35*Power(Cos(th),3))/16 + (63*Power(Cos(th),5))/16 - (33*Power(Cos(th),7))/16))/Power(r,6);
	}

 double sf24(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*((-35*Cos(th))/128 + (105*Power(Cos(th),3))/32 - (693*Power(Cos(th),5))/64 + (429*Power(Cos(th),7))/32 - (715*Power(Cos(th),9))/128))/Power(r,6);
	}

 double sf25(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,7)*((5*Cos(th))/16 - (35*Power(Cos(th),3))/16 + (63*Power(Cos(th),5))/16 - (33*Power(Cos(th),7))/16);
	}

 double sf26(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,9)*((5*Cos(th))/16 - (35*Power(Cos(th),3))/16 + (63*Power(Cos(th),5))/16 - (33*Power(Cos(th),7))/16);
	}

 double sf27(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(-0.0390625 + (35*Power(Cos(th),2))/32 - (315*Power(Cos(th),4))/64 + (231*Power(Cos(th),6))/32 - (429*Power(Cos(th),8))/128))/Power(r,7);
	}

 double sf28(double r, double th)
	{
		 return (Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*(0.02734375 - (315*Power(Cos(th),2))/256 + (1155*Power(Cos(th),4))/128 - (3003*Power(Cos(th),6))/128 + (6435*Power(Cos(th),8))/256 - (2431*Power(Cos(th),10))/256))/Power(r,7);
	}

 double sf29(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,8)*(-0.0390625 + (35*Power(Cos(th),2))/32 - (315*Power(Cos(th),4))/64 + (231*Power(Cos(th),6))/32 - (429*Power(Cos(th),8))/128);
	}

 double sf30(double r, double th)
	{
		 return Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))*Power(1 - Power(E,(2*(-1 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2)))/(-3 + (Power(r,2)*Power(Cos(th),2))/4 + Power(r,2)*Power(Sin(th),2))),2)*Power(r,10)*(-0.0390625 + (35*Power(Cos(th),2))/32 - (315*Power(Cos(th),4))/64 + (231*Power(Cos(th),6))/32 - (429*Power(Cos(th),8))/128);
	}

 void CreateVectorOfSimpleFunctions(vector<coordFunction>& functions)
{
	functions.push_back(sf1);
	functions.push_back(sf2);
	functions.push_back(sf3);
	functions.push_back(sf4);
	functions.push_back(sf5);
	functions.push_back(sf6);
	functions.push_back(sf7);
	functions.push_back(sf8);
	functions.push_back(sf9);
	functions.push_back(sf10);
	functions.push_back(sf11);
	functions.push_back(sf12);
	functions.push_back(sf13);
	functions.push_back(sf14);
	functions.push_back(sf15);
	functions.push_back(sf16);
	functions.push_back(sf17);
	functions.push_back(sf18);
	functions.push_back(sf19);
	functions.push_back(sf20);
	functions.push_back(sf21);
	functions.push_back(sf22);
	functions.push_back(sf23);
	functions.push_back(sf24);
	functions.push_back(sf25);
	functions.push_back(sf26);
	functions.push_back(sf27);
	functions.push_back(sf28);
	functions.push_back(sf29);
	functions.push_back(sf30);
}