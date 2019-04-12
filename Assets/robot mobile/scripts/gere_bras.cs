// Gestion du bras articulé
// script à appliquer à O0 (base du robot mobile)
// Pré: Vector3 increment, à modifier par le script d'I3D choisi
// Post: Mise à jour de la position de la pince (Opince)
//       Si mise à jour pas possible, Ok=false, num_axe_butee est mis à jour
//       Si mise à jour possible, Ok=true, num_axe_butee=0
//
// Fonction à utiliser de l'extérieur du script: Vector3 get_Opince_absolute_position() <==> position de la pince dans le repère du monde
//                                            et Vector3 get_Opince_local_position() <==> position de la pince dans le repère lié à O0 (base mobile)

using UnityEngine;
using System.Collections;


public class gere_bras : MonoBehaviour {

public  GameObject O1tierce;
public GameObject O2tierce;
public GameObject O3tierce;
public GameObject O4tierce;
public GameObject O5tierce;
public GameObject O6tierce;
public GameObject Opince;
public Vector3 increment; // Incrément à appliquer à Opince, si possible
public bool Ok; // Résultat suite à l'application de l'incrément, Ok=false=>axe en butée
public int num_axe_butee; // Si Ok=false, numéro du premier axe trouvé en butée (1 à 6) ou -1 si increment trop important, sinon vaut 0
public bool DEBUG=false; // Si true, affichage des données en position de Opince ainsi que des données articulaires liées à O1tierce,...,O6tierce

private Vector3 last_q1q2q3; // variables articulaires calculées du bras articulé à t-1
private Vector3 current_q1q2q3; // variables articulaires calculées du bras articulé à t
private Vector3 desired_q1q2q3; // variables articulaires désirées du bras articulé à t
private double[] angles; // tableau des valeurs des angles pour chaque axe du robot (en degrés)
private double[] limit; // Valeur limite sur les positions angulaires pour chaque axe du robot (en degrés)
private Vector3 last_effector_position;
private Vector3 current_effector_position;

    void Awake()
    {
	  Vector3 init_effector_position;
	
	  DEBUG = true; // Mode DEBUG, affichage sur la console
	
	  angles = new double[6];
	  limit = new double[6]; // Tableau des limites angulaires pour chaque axe du robot (degrés)
	
	  limit[0] = 50.0;
	  limit[1] = 75.0;
	  limit[2] = 55.0;
	  limit[3] = 380.0;
	  limit[4] = 240.0;
	  limit[5] = 720.0;
	
	  init_effector_position = new Vector3(0.44F,0.365F,0.0F); // Position initiale de Opince dans le repère lié à O0

	  current_q1q2q3 = new Vector3(0.0F,0.0F,0.0F);
	  last_q1q2q3 = new Vector3(0.0F,0.0F,0.0F);
	
	  last_effector_position = init_effector_position;
	  calc_angles( init_effector_position);
    }

//------------------------------------------------------
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        Vector3 new_position;

        if (Input.GetKey(KeyCode.R)) // Effectue un Reset de la position de la pince.
        {
            calc_angles(new Vector3(0.44F, 0.365F, 0.0F));
            increment = new Vector3(0.0F, 0.0F, 0.0F);
            last_effector_position = new Vector3(0.44F, 0.365F, 0.0F);
            calc_angles(last_effector_position);
        }

        if (increment.x * increment.x + increment.y * increment.y + increment.z * increment.z > 0)
        {
            increment.z = -increment.z;
            new_position = increment + last_effector_position;
            if (calc_angles(new_position) == true)
            {
                last_effector_position = new_position;
                Ok = true;
            }
            else
                Ok = false;

            increment = new Vector3(0.0F, 0.0F, 0.0F);
        }
    }

    //------------------------------------------------------
    // Position de Opince dans le repère absolue, lié au monde virtuel
    public Vector3 get_Opince_absolute_position()
    {
        Vector3 absolute_position = new Vector3( Opince.transform.position.x, Opince.transform.position.y, Opince.transform.position.z);
        return absolute_position;
    }
    //------------------------------------------------------
    // Position de Opince dans le repère lié à O0
    public Vector3 get_Opince_local_position()
    {
        Vector3 local_position = new Vector3();
        Transform Opince_parent = Opince.transform.parent;
        Opince.transform.parent = O1tierce.transform.parent.transform.parent.transform.parent; // Opince devient le fils de O0

        local_position = Opince.transform.localPosition;
        Opince.transform.parent = Opince_parent;
        return local_position;
    }
    //------------------------------------------------------
    // Pré: Nouvelle Position 3D désirée de la pince, à l'instant t, DANS LE REPERE LIE A O0
    // Post: error = true si déplacement pas possible, = false sinon
    //       
bool calc_angles(Vector3 desired_effector_position)
{
	bool allok;
	
	allok = calc_q1q2q3( desired_effector_position);
	
	if (allok == false)
		return false;
		
	Q1Q2Q3_to_angles( desired_q1q2q3);
	
	allok = verify_angles_constraint( );
	if (allok == false)
		return false;
	
	last_q1q2q3 = current_q1q2q3;
	current_q1q2q3 = desired_q1q2q3;
	
	maj_bras_robot();

    num_axe_butee = 0;
	return true;
}

//------------------------------------------------------

bool calc_q1q2q3(Vector3 desired_effector_position)
{
	double L1;
    double d2;
    double d3;
    double d4;
    double r4;
    double Lpince;
    double Px;
    double Py;
    double Pz;
    double delta;
    double cdelta;
    double sdelta;
    double q1;
    double q1prime;
    double c1;
    double s1;
    double X;
    double Z;
    double Y;
    double c3delta;
    double s3delta;
    double q3;
    double A;
    double B;
    double s2;
    double c2;
    double q2;
    double last_q3;
    double last_q2;
    double last_q1;
	string aux;

	L1 = 0.350;
	d2 = 0.150;
	d3 = 0.250;
	d4 = 0.075;
	r4 = 0.290;
	Lpince = 0.27 + 0.04;

	Px = desired_effector_position.x;
    Py = desired_effector_position.z;
    Pz = desired_effector_position.y;

	if (DEBUG == true)
	{
		aux = "Opince désiré = [" + Px.ToString("0.0000") + "," + Pz.ToString("0.0000") + "," + (-Py).ToString("0.0000") + "] dans le repère lié à O0";
		Debug.Log(aux);
	}

	delta = System.Math.Atan(r4/d4);
	cdelta = System.Math.Cos(delta);
	sdelta = System.Math.Sin(delta);

	q1 = System.Math.Atan( Py/Px);
	
	q1prime = q1+System.Math.PI;
	last_q1 = last_q1q2q3.x;

	if (q1 - last_q1 > System.Math.PI/2)
   		q1 = q1 - System.Math.PI;
	if (q1 - last_q1 < -System.Math.PI/2)
   		q1 = q1 + System.Math.PI;

	c1 = System.Math.Cos(q1);
	s1 = System.Math.Sin(q1);
	X = Px/c1 - d2;
	Z = Pz - L1 + Lpince;
	Y = d4/cdelta;

	c3delta = (Z*Z + X*X - Y*Y - d3*d3)/(2*d3*Y);
    if (c3delta > 1.0F)
    {
        if (DEBUG==true)
            Debug.Log("c3delta=" + c3delta.ToString()+" > 1");
        num_axe_butee = -1;
        return false;
    }
	s3delta = System.Math.Sqrt(1.0F - c3delta*c3delta);
   	q3 = -System.Math.Atan(s3delta/c3delta) + delta;

    if (DEBUG == true)
        Debug.Log("q3 pré: " + q3.ToString());

   	last_q3 = last_q1q2q3.z;
    if (DEBUG==true)
        Debug.Log("last_q3: " + last_q3.ToString());
	if (q3 - last_q3 > System.Math.PI/2)
   		q3 = q3 - System.Math.PI;
	if (q3 - last_q3 < -System.Math.PI/2)
   		q3 = q3 + System.Math.PI;
    if (DEBUG==true)
        Debug.Log("q3 post: " + q3.ToString());

	A = Y*c3delta + d3;
	B = -Y*s3delta;

	s2 = (A*X + B*Z)/(A*A + B*B);
	c2 = (A*Z - B*X) / (A*A + B*B);
	q2 = System.Math.Atan((A*X + B*Z)/ (A*Z - B*X));

	last_q2 = last_q1q2q3.y;

	if (q2 - last_q2 > System.Math.PI/2)
   		q2 = q2 - System.Math.PI;
	if (q2 - last_q2 < -System.Math.PI/2)
   		q2 = q2 + System.Math.PI;


	desired_q1q2q3 = new Vector3((float)q1,(float)q2,(float)q3);
	
	if (DEBUG == true)
	{
	 aux = "q1,q2,q3 = [" + q1.ToString("0.0000") + "," + q2.ToString("0.0000") + "," + q3.ToString("0.0000") + "]";
	 Debug.Log(aux);
	}
	
	return true;

}

    //------------------------------------------------------
// Transforme les données angulaires de q1, q2 et q3 (radians) en données angulaires sur les 6 angles (degrés)

void Q1Q2Q3_to_angles( Vector3 Q1Q2Q3 )
{
	angles[0] = (float) (Q1Q2Q3.x * 180.0 / Mathf.PI);
	angles[1] = (float) (-Q1Q2Q3.y * 180.0 / Mathf.PI);
	angles[2] = (float) (Q1Q2Q3.z * 180.0 / Mathf.PI);
	angles[3] = 0.0F;
	angles[4] = -angles[1] - angles[2] - 90.0F;
	angles[5] = angles[0];
}

//------------------------------------------------------
bool verify_angles_constraint()
{
    for (int i = 0; i < 6; i++)
        if ((angles[i] < -limit[i]) || (angles[i] > limit[i]))
        {
            if (DEBUG==true)
                Debug.Log("Axe "+i+" en butée!!! Valeur="+angles[i].ToString()+"°, Valeur absolue maximale="+limit[i].ToString()+"°");
            num_axe_butee = i + 1;
            return false;
        }

    return true;
}

//------------------------------------------------------

void maj_bras_robot()
{
    Vector3 Opince_absolute_position=new Vector3();
    Vector3 Opince_local_position = new Vector3();
    string aux;
    //aux = "Angles = [" + angles[0].ToString("000.00") + "," + angles[1].ToString("000.00") + "," +angles[2].ToString("000.00") + "," + angles[3].ToString("000.00") + "," + angles[4].ToString("000.00") + "," + angles[5].ToString("000.00") + "]";
  if (DEBUG == true)
  {
      Debug.Log("Axe 1: " + angles[0].ToString());
      Debug.Log("Axe 2: " + angles[1].ToString());
      Debug.Log("Axe 3: " + angles[2].ToString());
      Debug.Log("Axe 4: " + angles[3].ToString());
      Debug.Log("Axe 5: " + angles[4].ToString());
      Debug.Log("Axe 6: " + angles[5].ToString());
  }
	O1tierce.transform.localEulerAngles = new Vector3(O1tierce.transform.localEulerAngles.x, (float)angles[0], O1tierce.transform.localEulerAngles.z);
    O2tierce.transform.localEulerAngles = new Vector3(O2tierce.transform.localEulerAngles.x, (float)angles[1], O2tierce.transform.localEulerAngles.z);
    O3tierce.transform.localEulerAngles = new Vector3(O3tierce.transform.localEulerAngles.x, (float)angles[2], O3tierce.transform.localEulerAngles.z);
    O4tierce.transform.localEulerAngles = new Vector3(O4tierce.transform.localEulerAngles.x, (float)angles[3], O4tierce.transform.localEulerAngles.z);
    O5tierce.transform.localEulerAngles = new Vector3(O5tierce.transform.localEulerAngles.x, (float)angles[4], O5tierce.transform.localEulerAngles.z);
    O6tierce.transform.localEulerAngles = new Vector3(O6tierce.transform.localEulerAngles.x, (float)angles[5], O6tierce.transform.localEulerAngles.z);

    Opince_absolute_position = get_Opince_absolute_position();
    Opince_local_position = get_Opince_local_position();
    if (DEBUG == true)
    {
        aux = "Opince = [" + Opince_absolute_position.x.ToString("0.0000") + "," + Opince_absolute_position.y.ToString("0.0000") + "," + Opince_absolute_position.z.ToString("0.0000") + "] dans le repère du monde.";
        Debug.Log(aux);
        aux = "Opince = [" + Opince_local_position.x.ToString("0.0000") + "," + Opince_local_position.y.ToString("0.0000") + "," + Opince_local_position.z.ToString("0.0000") + "] dans le repère lié à O0.";
        Debug.Log(aux);
    }
}
}
