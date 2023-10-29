using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking.Types;

public class NGHelper : MonoBehaviour
{
	public io.newgrounds.core ngio_core;
	//string appID = "56110:Lnuvw67a";
	//string aesKey = "lt1FajqUmgZ7vJQkY1tMRw==";
	void Start() {
		ngio_core.onReady(() => {
			ngio_core.checkLogin((bool logged_in) => {
				if (logged_in) {
					onLoggedIn();
				}// else {
				 //   requestLogin();
				 //}
			});
		});
	}

	void onLoggedIn() {
		io.newgrounds.objects.user player = ngio_core.current_user;
	}
	void requestLogin() {
		ngio_core.requestLogin(onLoggedIn, onLoginFailed, onLoginCancelled);
	}
	void onLoginFailed() {
		io.newgrounds.objects.error error = ngio_core.login_error;
		print("NG io error: " + error);
	}
	void onLoginCancelled() {
		print("NG io login canceled");
	}

	/* Example medal
	 * 
	public void UnlockMedalColette() {

		io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();
		medal_unlock.id = 73689;

		medal_unlock.callWith(ngio_core);
		print("Medal Unlocked");
	}
	*/
    public void UnlockMedal(string medal) {

        io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();
		if(medal == "prison") {
			medal_unlock.id = 75906;
		} else if(medal == "turret") {
            medal_unlock.id = 75907;
        } else if (medal == "someplaceElse") {
            medal_unlock.id = 75908;
        } else if (medal == "repeat") {
            medal_unlock.id = 75909;
        } else if (medal == "laser") {
            medal_unlock.id = 75910;
        } else if (medal == "somos") {
            medal_unlock.id = 75911;
        } else if (medal == "theend") {
            medal_unlock.id = 75912;
        } else {
			print("Medal not found");
		}

        medal_unlock.callWith(ngio_core);
        print("Medal Unlocked: " + medal);
    }
}
