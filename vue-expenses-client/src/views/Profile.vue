<template>
  <div>
    <v-container>
      <v-layout row justify-space-around>
        <v-flex xs12 sm8>
          <v-card class="pa-2 mr-2" flat height="100%">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Profile</div>
            <v-divider></v-divider>
            <v-form class="xs12">
              <v-container>
                <v-text-field
                  label="First Name"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                <v-text-field
                  label="Last Name"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                <v-text-field label="Email" class="ma-0 pa-0 form-label" dense readonly></v-text-field>
                <v-switch class="ma-0 pa-0 form-label" color="grey" label="Use Dark Mode"></v-switch>
                <v-row class="justify-end">
                  <v-btn outlined small class="blue--text font-weight-bold">Submit</v-btn>
                </v-row>
              </v-container>
            </v-form>
          </v-card>
        </v-flex>
      </v-layout>
      <v-layout mt-3 row justify-space-around>
        <v-flex xs12 sm8>
          <v-card class="pa-2 mr-2" flat height="100%">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Security</div>
            <v-divider></v-divider>
            <v-form class="xs12">
              <v-container>
                <v-text-field
                  label="Current Password"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                <v-text-field
                  label="New Password"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                <v-text-field
                  label="Retype Password"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                
                <v-row class="justify-end">
                  <v-btn outlined small class="blue--text font-weight-bold">Submit</v-btn>
                </v-row>
              </v-container>
            </v-form>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
    
  </div>
</template>
<script>
export default {};
</script>

<style>
</style> 